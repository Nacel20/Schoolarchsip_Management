using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolarshipManagement.DataAccess.IRepository;
using SchoolarshipManagement.Models.Models;
using SchoolarshipManagement.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolarshipManagement.Controllers
{
    public class CityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            CityVM cityVM = new CityVM()
            {
                City = new City(),
                CountryList = _unitOfWork.CountryRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            cityVM.City = _unitOfWork.CityRepository.Get(id);
            if (cityVM.City == null)
            {
                return NotFound();
            }
            return View(cityVM);
        }

        public IActionResult Upsert(int? id)
        {
            CityVM cityVM = new CityVM()
            {
                City = new City(),               
                CountryList = _unitOfWork.CountryRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            if (id == null)
            {
                //this is for create
                return View(cityVM);
            }
            //this is for edit
            cityVM.City = _unitOfWork.CityRepository.Get(id.GetValueOrDefault());
            if (cityVM.City == null)
            {
                return NotFound();
            }
            return View(cityVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CityVM cityVM)
        {
            if (ModelState.IsValid)
            { 
                if (cityVM.City.Id == 0)
                {
                    _unitOfWork.CityRepository.Add(cityVM.City);
                }
                else
                {
                    _unitOfWork.CityRepository.Update(cityVM.City);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {                
                cityVM.CountryList = _unitOfWork.CountryRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
               
                if (cityVM.City.Id != 0)
                {
                    cityVM.City = _unitOfWork.CityRepository.Get(cityVM.City.Id);
                }
            }
            return View(cityVM);
        }



        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.CityRepository.GetAll(includeProperties: "Country");
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.CityRepository.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.CityRepository.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
