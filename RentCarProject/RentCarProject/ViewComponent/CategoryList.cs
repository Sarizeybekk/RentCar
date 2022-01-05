using Microsoft.AspNetCore.Mvc;
using RentCarProject.DataAccess.Repository;
using RentCarProject.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarProject.NewComponents
{
    public class CategoryList: ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryList(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IViewComponentResult Invoke()
        {
            var category = _unitOfWork.Category.GetAll();
            return View(category);
        }
        
    }
}
