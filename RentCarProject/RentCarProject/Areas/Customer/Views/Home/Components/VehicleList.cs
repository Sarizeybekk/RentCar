using Microsoft.AspNetCore.Mvc;
using RentCarProject.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarProject.Areas.Customer.Views.Home.Components
{
    public class VehicleList:ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public VehicleList(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IViewComponentResult Invoke(int? KategoryId)
        {
            if (KategoryId.HasValue)
            {
                return View(_unitOfWork.Vehicle.GetVehicleByCategory((int)KategoryId));
            }
            var vehicle = _unitOfWork.Vehicle.GetAll();
            return View(vehicle);
        }
    }
}
