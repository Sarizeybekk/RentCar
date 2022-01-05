using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarProject.DataAccess.Repository.IRepository
{
    public  interface IUnitOfWork:IDisposable
    {
        ICategoryRepository Category { get; }
        IVehicleRepository Vehicle { get; }
        IApplicationUserRepository ApplicationUser{ get; }

        IReservationCartRepository ReservationCart { get; }
       
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailsRepository OrderDetails { get; }

        ISP_Call SP_Call { get; }
        void Save();
    }
}