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
    public class CountryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CountryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            Country country = new Country();
            country = _unitOfWork.CountryRepository.Get(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        public IActionResult Upsert(int? id)
        {
            Country country = new Country();
            if (id == null)
            {
                //this is for create
                return View(country);
            }
           
            //this is for edit
            country = _unitOfWork.CountryRepository.Get(id.GetValueOrDefault());
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Country country)
        {
            if (ModelState.IsValid)
            {
                if (country.Id == 0)
                {
                    _unitOfWork.CountryRepository.Add(country);
                }
                else
                {
                    _unitOfWork.CountryRepository.Update(country);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.CountryRepository.GetAll();
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.CountryRepository.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.CountryRepository.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
