using RentCarProject.Data;
using RentCarProject.DataAccess.Repository.IRepository;
using RentCarProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarProject.DataAccess.Repository
{
    public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderDetailsRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public void Update(OrderDetails nesne)
        {
            _db.Update(nesne);
        }
    }
}
