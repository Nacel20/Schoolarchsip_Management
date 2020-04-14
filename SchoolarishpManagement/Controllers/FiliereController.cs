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
    public class FiliereController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FiliereController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            Filiere filiere = new Filiere();
            filiere = _unitOfWork.FiliereRepository.Get(id);
            if (filiere == null)
            {
                return NotFound();
            }
            return View(filiere);
        }

        public IActionResult Upsert(int? id)
        {
            Filiere filiere = new Filiere();
            if (id == null)
            {
                //this is for create
                return View(filiere);
            }
           
            //this is for edit
            filiere = _unitOfWork.FiliereRepository.Get(id.GetValueOrDefault());
            if (filiere == null)
            {
                return NotFound();
            }
            return View(filiere);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Filiere filiere)
        {
            if (ModelState.IsValid)
            {
                if (filiere.Id == 0)
                {
                    _unitOfWork.FiliereRepository.Add(filiere);
                }
                else
                {
                    _unitOfWork.FiliereRepository.Update(filiere);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(filiere);
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.FiliereRepository.GetAll();
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.FiliereRepository.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.FiliereRepository.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
