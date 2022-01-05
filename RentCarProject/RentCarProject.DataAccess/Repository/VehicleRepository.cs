using Microsoft.EntityFrameworkCore;
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
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        private readonly ApplicationDbContext _db;
        public VehicleRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public void Update(Vehicle vehicle)
        {
            var nesne = _db.Vehicles.FirstOrDefault(i=>i.Id== vehicle.Id);
            if(nesne!=null)
            {
                if (vehicle.İmageUrl!=null)
                {
                    nesne.İmageUrl = vehicle.İmageUrl;
                }
                nesne.CarName = vehicle.CarName;
                nesne.CarModel = vehicle.CarModel;
                nesne.DriverLisenseAge = vehicle.DriverLisenseAge;
                nesne.MinimumAgeLimit = vehicle.MinimumAgeLimit;
                nesne.DailyKmLimit = vehicle.DailyKmLimit;
                nesne.CurrentKm = vehicle.CurrentKm;
                nesne.AirBag = vehicle.AirBag;
                nesne.SeatCount = vehicle.SeatCount;
                nesne.DailyRentalPrice = vehicle.DailyRentalPrice;
                nesne.Quantity = vehicle.Quantity;
                nesne.CategoryId = vehicle.CategoryId;

            }
        }
        public List<Vehicle> GetVehicleByCategory(int id)
        {
            var vehicle = _db.Vehicles.AsQueryable();
            if (id!=null)
            {
                vehicle = vehicle.Include(i => i.Category).Where(a => a.CategoryId == id);
            }
            return vehicle.ToList();
        }
    }
}
