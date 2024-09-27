namespace DAL.Entities
{
    public abstract class BaseClass
    {
        public int Id {  get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; }
        
    }
}
