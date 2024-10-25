using BLL.Interfaces;
using DAL.Entities;
using DAL.OrderManagementDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repos
{
    public class CartRepo : GenericRepo<ShoppingCart>, ICartRepo
    {
        public CartRepo(OrderManagementDBContext context) : base(context)
        {
        }
    }
}
