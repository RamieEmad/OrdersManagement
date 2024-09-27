namespace DAL.Entities
{
    public class UploadFile : BaseClass
    {
        public string ImageUrl { get; set; }

        //To-Combine
        public string ContentType { get; set; }
        public string FileName { get; set; }
        
        //FK
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
