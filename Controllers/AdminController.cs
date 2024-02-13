using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VutaCheck01.Models;

namespace VutaCheck01.Controllers
{
    public class AdminController : Controller
    {
        AdminModel objAdminModel = new AdminModel();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AdminHomePage()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddVitaminFacts()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewVitaFacts(string NewVitaminfacts)
        {
            AdminModel obj = new AdminModel();
            return new JsonResult { Data= obj.AddAddNewVitaFacts(NewVitaminfacts) };
             
        }
        [HttpPost]
        public ActionResult GetCurrentVitaminFacts()
        {
            AdminModel obj = new AdminModel();
            return new JsonResult { Data = obj.GetCurrentVitaminFacts() };
        }
        [HttpPost]
        public ActionResult DeleteVitFact(int VitaminFactId)
        {
            return new JsonResult { Data = objAdminModel.DeleteVitFact(VitaminFactId) };
        }

        [HttpGet]
        public ActionResult AddSymptoms()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetVitaminList()
        {
            return new JsonResult { Data = objAdminModel.GetVitaminList() };
        }
        [HttpPost]
        public ActionResult AddNewSymptom(string NewSymptom,int VitaminID)
        {
            return new JsonResult { Data = objAdminModel.AddNewSymptom(NewSymptom, VitaminID)};
        }

        [HttpGet]
        public ActionResult EditorDeleteSymptoms()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetSymptomList()
        {
            return new JsonResult { Data = objAdminModel.GetSymptomList() };
        }
        [HttpPost]
        public ActionResult UpdateSymptom(int SymptomId,string UpdatedSymptom,int updatedVitaminId)
        {
            return new JsonResult { Data = objAdminModel.UpdateSymptom(SymptomId, UpdatedSymptom, updatedVitaminId) };
        }
        [HttpPost]
        public ActionResult DeleteSymptom(int DeleteSymptomId, int DeleVitaminId)
        {
            return new JsonResult { Data = objAdminModel.DeleteSymptom(DeleteSymptomId, DeleVitaminId) };
        }

        [HttpGet]
        public ActionResult AddVitaminSource()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewVitaminSourcseDet(int VitSourceType,string NewVitaSource,int VitaminId)
        {
            return new JsonResult { Data = objAdminModel.AddNewVitaminSourcseDet(VitSourceType, NewVitaSource, VitaminId) };
        }
        [HttpGet]
        public ActionResult EditorDeleteVitaminSource()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetVitaminSourceList(int VitSourceType, int VitaminId)
        {
            return new JsonResult { Data = objAdminModel.GetVitaminSourceList(VitSourceType, VitaminId) };
        }
        [HttpPost]
        public ActionResult EditVItaminSource(int VItaSourceTypeId,int VitSourceId,int EDVitaminId,string EditedFoodSource)
        {
            return new JsonResult { Data = objAdminModel.EditVItaminSource(VItaSourceTypeId, VitSourceId, EDVitaminId, EditedFoodSource) };

        }
        [HttpPost]
        public ActionResult DeleteFoodSource(int DeleteVItaSourceTypeId,int DeleteVitSourceId)
        {
            return new JsonResult { Data = objAdminModel.DeleteFoodSource(DeleteVItaSourceTypeId, DeleteVitSourceId)};
        }

    }
}