using Domain;

namespace Application.Service
{
    public class DocumentVM
    {
        public int Id { get; set; }

        public string FileName { get; set; }
        
        public string Code { get; set; }

        public string Rev { get; set; }

        public string Title { get; set; }

        public string DocType { get; set; }
    }
}
