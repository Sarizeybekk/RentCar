using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RentCarProject.DataAccess.Repository.IRepository;
using RentCarProject.Diger;
using RentCarProject.Models;
using RentCarProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RentCarProject.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Search(string q)
        {
            if (!string.IsNullOrEmpty(q))
            {
                var vehicles = _unitOfWork.Vehicle.SearchVehicle(q);
                return View(vehicles);
            }
            return View();
        }
        public IActionResult CategoryDetails(int? id)
        {
            ViewBag.KategoryId = id;
            return View();
        }

        public IActionResult Index()
        {
            IEnumerable<Vehicle> vehicleList = _unitOfWork.Vehicle.GetAll(includeProperties: "Category");

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim!=null)
            {
                var count = _unitOfWork.ReservationCart.GetAll(i => i.ApplicationUserId == claim.Value).ToList().Count();
                HttpContext.Session.SetInt32(SD.rrReservationCart, count);
            }


            return View(vehicleList);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            var vehicle = _unitOfWork.Vehicle.GetFirstOrDefault(i=>i.Id==id,includeProperties:"Category");
            ReservationCart cart = new ReservationCart()
            {
                Vehicle = vehicle,
                VehicleId = vehicle.Id
            };

            return View(cart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ReservationCart Rcart)
        {
            Rcart.Id = 0;
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                Rcart.ApplicationUserId = claim.Value;
                ReservationCart cartFromDb = _unitOfWork.ReservationCart.GetFirstOrDefault(
                    u => u.ApplicationUserId == Rcart.ApplicationUserId &&
                    u.VehicleId == Rcart.VehicleId,
                    includeProperties:"Vehicle"
                    );
                if (cartFromDb==null)
                {
                    _unitOfWork.ReservationCart.Add(Rcart);
                }
                else
                {
                    cartFromDb.Count += Rcart.Count;
                }
                _unitOfWork.Save();
                var count = _unitOfWork.ReservationCart.GetAll(i => i.ApplicationUserId == Rcart.ApplicationUserId).ToList().Count;
                HttpContext.Session.SetInt32(SD.rrReservationCart, count);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var vehicle = _unitOfWork.Vehicle.GetFirstOrDefault(i => i.Id == Rcart.Id, includeProperties: "Category");
                ReservationCart cart = new ReservationCart()
                {
                    Vehicle = vehicle,
                    VehicleId = vehicle.Id
                };
            }
          

            return View(Rcart);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
