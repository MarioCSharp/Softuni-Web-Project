using BetterShkolo.Data.Models;
using BetterShkolo.Data;
using BetterShkolo.Models.Newspapper;
using BetterShkolo.Services.AccountService;
using BetterShkolo.Services.SchoolService;
using Microsoft.EntityFrameworkCore;
using System.Buffers.Text;

namespace BetterShkolo.Services.NewspapperService
{
    public class NewspapperService : INewspapperService
    {
        private ApplicationDbContext context;
        private IAccountService accountService;
        private ISchoolService schoolService;
        public NewspapperService(ApplicationDbContext context,
                                 IAccountService accountService,
                                 ISchoolService schoolService)
        {
            this.context = context;
            this.accountService = accountService;
            this.schoolService = schoolService;
        }

        public async Task<List<NewspaperIndexModel>> GetNews()
        {
            var sId = await schoolService.GetSchoolIdByUser();

            return await context.Posts
                .Where(x => x.SchoolId == sId)
                .Select(x => new NewspaperIndexModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Content,
                    Date = x.DateAdded.ToString("dd MMM").ToUpper(),
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(x.Image))
                }).ToListAsync();
        }

        public async Task<NewspaperIndexModel> GetPost(int id)
        {
            var post = await context.Posts.FindAsync(id);

            return new NewspaperIndexModel
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Content,
                Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(post.Image)),
                Date = post.DateAdded.ToString()
            };
        }

        public async Task<bool> PostAsync(List<IFormFile> Image, NewspapperAddModel model)
        {
            var post = new Post()
            {
                Title = model.Title,
                Content = model.Description,
                DateAdded = DateTime.Now,
                UserId = accountService.GetUserId(),
                SchoolId = await schoolService.GetSchoolIdByUser()
            };

            foreach (var d in Image)
            {
                if (d.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await d.CopyToAsync(stream);
                        post.Image = stream.ToArray();
                    }
                }
            }

            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();

            return await context.Posts.ContainsAsync(post);
        }
    }
}
