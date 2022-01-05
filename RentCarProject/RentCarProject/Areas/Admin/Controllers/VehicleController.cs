using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentCarProject.DataAccess.Repository.IRepository;
using RentCarProject.Models;
using RentCarProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VehicleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        IWebHostEnvironment _hostEnvironment;

        public VehicleController(IUnitOfWork unitOfWork,IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            VehicleVM vehicleVM = new VehicleVM() {
                Vehicle = new Vehicle(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            if (id==null)
            {
                return View(vehicleVM);
            }
            vehicleVM.Vehicle = _unitOfWork.Vehicle.Get(id.GetValueOrDefault());
            if (vehicleVM.Vehicle == null)
            {
                return NotFound();
            }
            return View(vehicleVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(VehicleVM vehicleVM)
        {
           
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count>0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath,@"images\vehicles");
                    var extenstion = Path.GetExtension(files[0].FileName);
                    if (vehicleVM.Vehicle.İmageUrl!=null)
                    {
                        var imagePath = Path.Combine(webRootPath, vehicleVM.Vehicle.İmageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion),
                        FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    vehicleVM.Vehicle.İmageUrl = @"\images\vehicles\" + fileName + extenstion;
                }
                //hangi araca tıklarsan o aracın resmi gelsin
                else
                {
                    if (vehicleVM.Vehicle.Id!=0)
                    {
                        var nesne = _unitOfWork.Vehicle.Get(vehicleVM.Vehicle.Id);
                        vehicleVM.Vehicle.İmageUrl = nesne.İmageUrl;
                  
                    }
                }

                if (vehicleVM.Vehicle.Id==0)
                {
                    _unitOfWork.Vehicle.Add(vehicleVM.Vehicle);
                }
                else
                {
                    _unitOfWork.Vehicle.Update(vehicleVM.Vehicle);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {

                vehicleVM.CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                if (vehicleVM.Vehicle.Id!=0)
                {
                    vehicleVM.Vehicle = _unitOfWork.Vehicle.Get(vehicleVM.Vehicle.Id);
                }
            }
            return View(vehicleVM);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var nesne = _unitOfWork.Vehicle.Get(id);
            if (nesne==null)
            {
                return Json(new { success = false, message = "Silme işlemi basarısız" });
            }
            string webRootPath = _hostEnvironment.WebRootPath;

            var imagePath = Path.Combine(webRootPath, nesne.İmageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }




            _unitOfWork.Vehicle.Remove(nesne);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Silme işlemi başarılı" });
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var nesne = _unitOfWork.Vehicle.GetAll(includeProperties:"Category");
            return Json(new { data=nesne });
        }
    }
}
