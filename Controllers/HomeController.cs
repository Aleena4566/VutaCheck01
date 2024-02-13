using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;
using VutaCheck01.Models;

namespace VutaCheck01.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult UserLogin()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserRegistration()
        {
            return View();

        }

        [HttpGet]
        public ActionResult ChooseSymptom()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetSymptomList()
        {
            HomeModel obj = new HomeModel();
            return new JsonResult { Data = obj.GetSymptomList() };
        }

        [HttpPost]
        public ActionResult GetDeficentVitaminDetail(string SelectedSymptomId)
        {
            HomeModel obj = new HomeModel();
            return new JsonResult { Data = obj.GetDeficentVitaminDetail(SelectedSymptomId) };

        }

        [HttpPost]
        public ActionResult GetNutritiousfood(int VitaminId)
        {
            HomeModel obj = new HomeModel();
            return new JsonResult { Data = obj.GetNutritiousfood(VitaminId) };
        }
        [HttpGet]
        public ActionResult CalculateBMI()
        {
            return View();
        }
        [HttpPost]
        public ActionResult JoinNewUser(string FirstName, string LastName, string Email, string UserName, string Password)
        {
            HomeModel obj = new HomeModel();
            return new JsonResult { Data = obj.JoinNewUser(FirstName, LastName, Email, UserName, Password) };
        }
        [HttpPost]
        public ActionResult CheckUserDetails(string UserName, string Password)
        {
            HomeModel obj = new HomeModel();
            return new JsonResult { Data = obj.CheckUserDetails(UserName, Password) };


        }
        [HttpGet]
        public ActionResult UserHomePage()
        {
            return View();
        }


        [HttpGet]
        public ActionResult EducationalContent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetEducationalContent()
        {
            HomeModel obj = new HomeModel();
            return new JsonResult { Data = obj.GetEducationalContent() };

        }

        [HttpGet]
        public ActionResult LogFoodIntake()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetFoodList(int SourceType)
        {
            HomeModel obj = new HomeModel();
            return new JsonResult { Data = obj.GetFoodList(SourceType) };
        }
        [HttpPost]
        public ActionResult LogFoodIntakeDetails(string IntakeDate,string IntakeTime,int SourceType,string AdditionalInfo,string SelectedSource,int UserId)
        {
            HomeModel obj = new HomeModel();            
            return new JsonResult { Data = obj.LogFoodIntakeDetails(IntakeDate, IntakeTime, SourceType, AdditionalInfo, SelectedSource, UserId) };

        }

        [HttpGet]
        public ActionResult FoodIntakeHistory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetFoodIntakeHistory(string FromDate,string ToDate, int SourceType,int UserId)
        {
            HomeModel obj = new HomeModel();
            return new JsonResult { Data = obj.GetFoodIntakeHistory(FromDate, ToDate, SourceType, UserId) };

        }

    }
}


