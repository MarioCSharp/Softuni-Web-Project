using BetterShkolo.Data;
using BetterShkolo.Data.Models;
using BetterShkolo.Models.Resource;
using Microsoft.Net.Http.Headers;

namespace BetterShkolo.Services.ResourceService
{
    public class ResourceService : IResourceService
    {
        private ApplicationDbContext context;
        public ResourceService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> AddResource(List<IFormFile> file, ResourceModel model)
        {
            if (file.Count > 0)
            {
                var r = new Resource()
                {
                    Name = model.Name,
                    LessonId = model.LessonId,
                };

                foreach (var d in file)
                {
                    if (d.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await d.CopyToAsync(stream);
                            r.File = stream.ToArray();
                            string fileName = ContentDispositionHeaderValue.Parse(d.ContentDisposition).FileName.ToString();
                            fileName = fileName.Trim('"');
                            r.FileExtension = Path.GetExtension(fileName);
                        }
                    }
                }

                r.Link = "";

                await context.Resources.AddAsync(r);
                await context.SaveChangesAsync();
            }

            if (!string.IsNullOrEmpty(model.Link))
            {
                var r = new Resource()
                {
                    Name = model.Name,
                    LessonId = model.LessonId,
                    Link = model.Link,
                    File = new byte[1],
                    FileExtension = ""
                };

                await context.Resources.AddAsync(r);
                await context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<Resource> GetResource(int resourceId)
        {
            return await context.Resources.FindAsync(resourceId);
        }
    }
}
