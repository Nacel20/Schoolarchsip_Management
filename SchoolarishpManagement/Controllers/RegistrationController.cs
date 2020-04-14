using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolarshipManagement.DataAccess.IRepository;
using SchoolarshipManagement.Models.Models;
using SchoolarshipManagement.Models.ViewModels;

namespace SchoolarshipManagement.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public RegistrationController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            RegistrationVM registrationVM = new RegistrationVM()
            {
                Registration = new Registration(),
                AcademicYearList = _unitOfWork.AcademicYearRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Code,
                    Value = i.Id.ToString()
                }),
                LevelList = _unitOfWork.LevelRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                FiliereList = _unitOfWork.FiliereRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            registrationVM.Registration = _unitOfWork.RegistrationRepository.Get(id);
            if (registrationVM.Registration == null)
            {
                return NotFound();
            }
            return View(registrationVM);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            RegistrationVM registrationVM = new RegistrationVM()
            {
                Registration = new Registration(),
                AcademicYearList = _unitOfWork.AcademicYearRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Code,
                    Value = i.Id.ToString()
                }),
                LevelList = _unitOfWork.LevelRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                FiliereList = _unitOfWork.FiliereRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            if (id == null)
            {
                //this is for create
                return View(registrationVM);
            }
            //this is for edit
            registrationVM.Registration = _unitOfWork.RegistrationRepository.Get(id.GetValueOrDefault());
            if (registrationVM.Registration == null)
            {
                return NotFound();
            }
            return View(registrationVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(RegistrationVM registrationVM)
        {
            if (ModelState.IsValid)
            {
                if (registrationVM.Registration.Id == 0)
                {
                    _unitOfWork.RegistrationRepository.Add(registrationVM.Registration);
                }
                else
                {
                    _unitOfWork.RegistrationRepository.Update(registrationVM.Registration);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                registrationVM.LevelList = _unitOfWork.LevelRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                registrationVM.FiliereList = _unitOfWork.FiliereRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                registrationVM.AcademicYearList = _unitOfWork.AcademicYearRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Code,
                    Value = i.Id.ToString()
                });
                if (registrationVM.Registration.Id != 0)
                {
                    registrationVM.Registration = _unitOfWork.RegistrationRepository.Get(registrationVM.Registration.Id);
                }
            }
            return View(registrationVM);
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.RegistrationRepository.GetAll(includeProperties: "Student,Level,Filiere,AcademicYear");
            return Json(new { data = allObj });
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.RegistrationRepository.Get(id);

            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.RegistrationRepository.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion

    }
}