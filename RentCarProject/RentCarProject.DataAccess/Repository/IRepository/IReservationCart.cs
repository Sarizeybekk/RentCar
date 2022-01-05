using RentCarProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarProject.DataAccess.Repository.IRepository
{
     public interface IReservationCartRepository:IRepository<ReservationCart>
    {
        void Update(ReservationCart nesne);
    }
}
