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
    public class LevelController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public LevelController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            Level level = new Level();
            level = _unitOfWork.LevelRepository.Get(id);
            if (level == null)
            {
                return NotFound();
            }
            return View(level);
        }

        public IActionResult Upsert(int? id)
        {
            Level level = new Level();
            if (id == null)
            {
                //this is for create
                return View(level);
            }
            
            //this is for edit
            level = _unitOfWork.LevelRepository.Get(id.GetValueOrDefault());
            if (level == null)
            {
                return NotFound();
            }
            return View(level);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Level level)
        {
            if (ModelState.IsValid)
            {
                if (level.Id == 0)
                {
                    _unitOfWork.LevelRepository.Add(level);
                }
                else
                {
                    _unitOfWork.LevelRepository.Update(level);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(level);
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.LevelRepository.GetAll();
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.LevelRepository.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.LevelRepository.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
