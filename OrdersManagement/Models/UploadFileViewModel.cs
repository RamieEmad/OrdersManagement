using DAL.Entities;
namespace PL.Models
{
    public class UploadFileViewModel
    {
        public IFormFile File { get; set; }

        //To-Combine
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public string ImageUrl { get; set; }


        //FK
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
