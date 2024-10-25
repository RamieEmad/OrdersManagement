using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IUploadFileRepo : IGenericRepo<UploadFile>
    {
        Task AddUploadFileAsync(UploadFile uploadFile);
        Task<ICollection<UploadFile>> GetUploadFilesByProductId(int id);
    }
}
