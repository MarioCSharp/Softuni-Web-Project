using Better_Shkolo.Data;

namespace Better_Shkolo.Services.DocumentService
{
    public class DocumentService : IDocumentService
    {
        private ApplicationDbContext context;
        public DocumentService(ApplicationDbContext context)
        {
            this.context = context;
        }


    }
}
