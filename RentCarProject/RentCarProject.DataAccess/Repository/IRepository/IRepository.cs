using RentCarProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentCarProject.DataAccess.Repository
{
     public interface IRepository<T> where T:class
    {
        T Get(int id);
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
            );
        T GetFirstOrDefault(
              Expression<Func<T, bool>> filter = null,
            string includeProperties = null
            );
        void Add(T entity);
        void Remove(int id);
        void Remove(T entity);//silinecek olan hangi entity ürün mü kategori mi mesela

        void RemoveRange(IEnumerable<T> entity);

        List<Vehicle> SearchVehicle(string q);

    }
}
