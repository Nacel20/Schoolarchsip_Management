using Microsoft.AspNetCore.Mvc;
using SchoolarshipManagement.DataAccess.IRepository;
using SchoolarshipManagement.Models.Models;

namespace SchoolarshipManagement.Controllers
{
    public class GenderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            Gender gender = new Gender();
            gender = _unitOfWork.GenderRepository.Get(id);
            if (gender == null)
            {
                return NotFound();
            }
            return View(gender);
        }

        public IActionResult Upsert(int? id)
        {
            Gender gender = new Gender();
            if (id == null)
            {
                //this is for create
                return View(gender);
            }
           
            //this is for edit
            gender = _unitOfWork.GenderRepository.Get(id.GetValueOrDefault());
            if (gender == null)
            {
                return NotFound();
            }
            return View(gender);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Gender gender)
        {
            if (ModelState.IsValid)
            {
                if (gender.Id == 0)
                {
                    _unitOfWork.GenderRepository.Add(gender);
                }
                else
                {
                    _unitOfWork.GenderRepository.Update(gender);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(gender);
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.GenderRepository.GetAll();
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.GenderRepository.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.GenderRepository.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
