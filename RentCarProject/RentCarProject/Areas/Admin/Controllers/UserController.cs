using Microsoft.AspNetCore.Mvc;
using RentCarProject.Data;
using RentCarProject.DataAccess.Repository.IRepository;
using RentCarProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
      public IActionResult LockUnLock([FromBody] string id)
        {
            var nesne = _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);
                if (nesne==null)
                {
                return Json(new { success = false, message = "Hesap açma/kapama sırasında hata" });
                }
            if (nesne.LockoutEnd!=null && nesne.LockoutEnd>DateTime.Now)
            {
                nesne.LockoutEnd = DateTime.Now;
            }
            else
            {
                nesne.LockoutEnd = DateTime.Now.AddYears(10);
            }
            _db.SaveChanges();

            return Json(new { success = true, message = "Başarılı" });
        }





        [HttpGet]
        public IActionResult GetAll()
        {

            var userList = _db.ApplicationUsers.ToList();
            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();
            foreach (var item in userList )
            {
                var roleId = userRole.FirstOrDefault(u => u.UserId == item.Id).RoleId;
                item.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;
            }
            return Json(new { data=userList });
        }
    }
}
