using BLL.Interfaces;
using DAL.Entities;
using DAL.OrderManagementDBContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace BLL.Repos
{
    public class UploadFileRepo : GenericRepo<UploadFile>, IUploadFileRepo
    {
        private readonly OrderManagementDBContext _context;
        private readonly IWebHostEnvironment _webHost;

        public UploadFileRepo(OrderManagementDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddUploadFileAsync(UploadFile uploadFile)
        {
            _context.UploadFiles.Add(uploadFile);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<UploadFile>> GetUploadFilesByProductId(int id)
        {
            return await _context.UploadFiles.Where(uf => uf.ProductId == id).ToListAsync();

        }

    }
}