using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolarshipManagement.DataAccess.IRepository;
using SchoolarshipManagement.Models.Models;
using SchoolarshipManagement.Models.ViewModels;

namespace SchoolarshipManagement.Controllers
{
    public class AcademicYearController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AcademicYearController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            AcademicYear academicYear = new AcademicYear();
            academicYear = _unitOfWork.AcademicYearRepository.Get(id);
            if (academicYear == null)
            {
                return NotFound();
            }
            return View(academicYear);
        }

        public IActionResult Upsert(int? id)
        {
            AcademicYear academicYear = new AcademicYear();
            if (id == null)
            {
                //this is for create
                return View(academicYear);
            }
           
            //this is for edit
            academicYear = _unitOfWork.AcademicYearRepository.Get(id.GetValueOrDefault());
            if (academicYear == null)
            {
                return NotFound();
            }
            return View(academicYear);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(AcademicYear academicYear)
        {
            if (ModelState.IsValid)
            {
                if (academicYear.Id == 0)
                {
                    _unitOfWork.AcademicYearRepository.Add(academicYear);
                }
                else
                {
                    _unitOfWork.AcademicYearRepository.Update(academicYear);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(academicYear);
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.AcademicYearRepository.GetAll();
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.AcademicYearRepository.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.AcademicYearRepository.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
