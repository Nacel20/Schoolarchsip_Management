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
    public class StudentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public StudentController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            StudentVM studentVM = new StudentVM()
            {
                Student = new Student(),
                GenderList = _unitOfWork.GenderRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CityList = _unitOfWork.CityRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CountryList = _unitOfWork.CountryRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            studentVM.Student = _unitOfWork.StudentRepository.Get(id);
            if (studentVM.Student == null)
            {
                return NotFound();
            }
            return View(studentVM);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
           // IEnumerable<Category> CatList = await _unitOfWork.Category.GetAllAsync();
            StudentVM studentVM = new StudentVM()
            {
                Student = new Student(),
                GenderList = _unitOfWork.GenderRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),                
                CountryList = _unitOfWork.CountryRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CityList = _unitOfWork.CityRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })

            };
            if (id == null)
            {
                //this is for create
                return View(studentVM);
            }
            //this is for edit
            studentVM.Student = _unitOfWork.StudentRepository.Get(id.GetValueOrDefault());
            if (studentVM.Student == null)
            {
                return NotFound();
            }
            return View(studentVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(StudentVM studentVM)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\students");
                    var extenstion = Path.GetExtension(files[0].FileName);

                    if (studentVM.Student.ImageUrl != null)
                    {
                        //this is an edit and we need to remove old image
                        var imagePath = Path.Combine(webRootPath, studentVM.Student.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    studentVM.Student.ImageUrl = @"\images\students\" + fileName + extenstion;
                }
                else
                {
                    //update when they do not change the image
                    if (studentVM.Student.Id != 0)
                    {
                        Student objFromDb = _unitOfWork.StudentRepository.Get(studentVM.Student.Id);
                        studentVM.Student.ImageUrl = objFromDb.ImageUrl;
                    }
                }


                if (studentVM.Student.Id == 0)
                {
                    _unitOfWork.StudentRepository.Add(studentVM.Student);

                }
                else
                {
                    _unitOfWork.StudentRepository.Update(studentVM.Student);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                //IEnumerable<Gender> GenderList = _unitOfWork.GenderRepository.GetAll();
                studentVM.GenderList = _unitOfWork.GenderRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                studentVM.CountryList = _unitOfWork.CountryRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                // A corriger : Ne doit afficher que les villes du pays selectionné.
                //studentVM.CityList = _unitOfWork.CityRepository.GetAll().Where(x => x.CountryId == studentVM.Student.CountryId).Select(i => new SelectListItem
 
                 studentVM.CityList = _unitOfWork.CityRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                if (studentVM.Student.Id != 0)
                {
                    studentVM.Student = _unitOfWork.StudentRepository.Get(studentVM.Student.Id);
                }
            }
            return View(studentVM);
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.StudentRepository.GetAll(includeProperties:"Gender,Country,City");
            return Json(new { data = allObj });
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.StudentRepository.Get(id);

            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            string webRootPath = _hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, objFromDb.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _unitOfWork.StudentRepository.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion

    }
}