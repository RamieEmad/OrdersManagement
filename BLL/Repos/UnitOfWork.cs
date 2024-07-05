using BLL.Interfaces;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repos
{
    public class UnitOfWork : IUnitOfWork
    {

        public IProductRepo ProductRepo {get;set;}

        public UnitOfWork(IProductRepo productRepo)
        {
            ProductRepo = productRepo;
        }
    }
}
