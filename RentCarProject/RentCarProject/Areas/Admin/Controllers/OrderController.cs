using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCarProject.DataAccess.Repository.IRepository;
using RentCarProject.Diger;
using RentCarProject.Models;
using RentCarProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RentCarProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public OrderDetailsVM OrderVM { get; set; }
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]

        public IActionResult GetOrderList(string status)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            IEnumerable<OrderHeader> orderHeadersList;
            if (User.IsInRole(SD.Role_Admin))
            {
                orderHeadersList = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");
            }
            else
            {
                orderHeadersList = _unitOfWork.OrderHeader.GetAll(
                    i => i.ApplicationUserId == claim.Value,
                    includeProperties: "ApplicationUser");
            }
            switch (status)
            {
                case "beklenen":
                    orderHeadersList = orderHeadersList.Where(x => x.OrderStatus == SD.Durum_Beklemede);
                    break;
                case "onaylanan":
                    orderHeadersList = orderHeadersList.Where(x => x.OrderStatus == SD.Durum_Onaylandı);
                    break;

                case "teslimyolda":
                    orderHeadersList = orderHeadersList.Where(x => x.OrderStatus == SD.Durum_Hazırlandı);
                    break;


                default:
                    break;
            }


            return Json(new { data = orderHeadersList });
        }






        [HttpPost]
        [Authorize(Roles =SD.Role_Admin)]
        public IActionResult Onaylandi()
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(i => i.Id == OrderVM.OrderHeader.Id
            
            );
            orderHeader.OrderStatus = SD.Durum_Onaylandı;
            _unitOfWork.Save();
            return RedirectToAction("Index"); 
        }
        [HttpPost]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult KargoyaVer()
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(i => i.Id == OrderVM.OrderHeader.Id

            );
            orderHeader.OrderStatus = SD.Durum_Hazırlandı;
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }







        public IActionResult Details(int id)
        {
            OrderVM = new OrderDetailsVM
            {
                OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(i => i.Id == id,
                includeProperties: "ApplicationUser"),
                OrderDetails = _unitOfWork.OrderDetails.GetAll(x => x.OrderId == id,
                includeProperties: "Vehicle")
            };
            return View(OrderVM);
        }





        
    }
}
