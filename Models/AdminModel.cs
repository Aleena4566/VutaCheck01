using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Web.Mvc;
using System.Web.Services.Description;
using VutaCheck01.Entity;
using System.Data.Entity.Migrations;
using Microsoft.Ajax.Utilities;

namespace VutaCheck01.Models
{
    public class AdminModel
    {
        VitaCheck_DBEntities ObjEntity = new VitaCheck_DBEntities();
        public object AddAddNewVitaFacts(string NewVitaminfacts)
        {
            try
            {
                VitaminFactDetail objvitaminFactDetail = new VitaminFactDetail { VitaminFact = NewVitaminfacts, IsVitaFactDelete= false };
                ObjEntity.VitaminFactDetails.Add(objvitaminFactDetail);
                ObjEntity.SaveChanges();

                return ("Vitamin Fact Added Successfully");

            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            
        }
        public object GetCurrentVitaminFacts()
        {
            var result = (from A in ObjEntity.VitaminFactDetails.Where(a=>a.IsVitaFactDelete==false) 
                          select new { A.VitaminFact,A.VitaminFactId  }).ToList();
            return result;

        }
        public object DeleteVitFact(int VitaminFactId)
        {
            try
            {
                if(ObjEntity.VitaminFactDetails.Any(x=>x.VitaminFactId==VitaminFactId))
                {
                    VitaminFactDetail ObjVitaFact = ObjEntity.VitaminFactDetails.Single(x => x.VitaminFactId == VitaminFactId);
                    ObjVitaFact.IsVitaFactDelete= true;
                    ObjEntity.SaveChanges();
                    return "Vitamin Fact Deleted Successfully";
                }
                else
                {
                    return "Error";
                }
              
            }
            catch(Exception ex)
            {
                return ex.Message;

            }
           
        }
        public object GetVitaminList()
        {
            var result = (from A in ObjEntity.VitaminDetails select new { A.VitaminId, A.Vitamin }).ToList();
            return result;
        }
        public object AddNewSymptom(string NewSymptom, int VitaminID)
        {
            try
            {
                if(!ObjEntity.Symptoms.Any(x=>x.SymptomDescription==NewSymptom))
                {
                    Symptom objSympton = new Symptom { SymptomDescription = NewSymptom, IsSymptomDelete=false };
                    ObjEntity.Symptoms.Add(objSympton);
                    ObjEntity.SaveChanges();

                    int SymptomId = objSympton.SymptomId;

                    VitaminSymptomRelation obj = new VitaminSymptomRelation { SymptomId = SymptomId, VitaminId = VitaminID, IsRelationDelete = false };
                    ObjEntity.VitaminSymptomRelations.Add(obj);
                    ObjEntity.SaveChanges();

                    return "Symptom Added";

                }
                else
                {
                    return "Symptom already exist";
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
             
        }
        public object GetSymptomList()
        {
            var result = (from A in ObjEntity.Symptoms.Where(x=>x.IsSymptomDelete==false)
                          from B in ObjEntity.VitaminSymptomRelations.Where(b=>b.SymptomId==A.SymptomId && b.IsRelationDelete == false )
                          from C in ObjEntity.VitaminDetails.Where(c=>c.VitaminId==B.VitaminId)
                          select new { A.SymptomId, A.SymptomDescription, C.VitaminId,C.Vitamin }).OrderBy(m=>m.Vitamin).ToList();
            return result;
        }
        public object UpdateSymptom(int SymptomId, string UpdatedSymptom,int updatedVitaminId)
        {
            try
            {
                if (ObjEntity.Symptoms.Any(y => y.SymptomId == SymptomId))
                {
                    Symptom objsymptom =  ObjEntity.Symptoms.Single(x=>x.SymptomId==SymptomId);
                    objsymptom.SymptomDescription = UpdatedSymptom;                     

                    VitaminSymptomRelation ObjVitSymRelation = ObjEntity.VitaminSymptomRelations.Single(y=>y.SymptomId==SymptomId);
                    ObjVitSymRelation.VitaminId = updatedVitaminId;
                    ObjEntity.SaveChanges();

                    return "Success";

                }
                else
                {
                    return "Error";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            } 
        }

        public object DeleteSymptom(int DeleteSymptomId, int DeleVitaminId)
        {
            try
            {
                if (ObjEntity.Symptoms.Any(y => y.SymptomId == DeleteSymptomId))
                {
                    Symptom objsymptom = ObjEntity.Symptoms.Single(x => x.SymptomId == DeleteSymptomId);
                    objsymptom.IsSymptomDelete=true;

                    VitaminSymptomRelation vitaminSymptomRelation = ObjEntity.VitaminSymptomRelations.Single(y => y.SymptomId == DeleteSymptomId && y.VitaminId == DeleVitaminId);
                    vitaminSymptomRelation.IsRelationDelete = true;

                    ObjEntity.SaveChanges();
                    return "Deleted Sucessfully"; 
                }
                else
                {
                    return "Error";
                } 

            }
            catch(Exception ex)
            {
                return "Error";
            }
        }
        public object AddNewVitaminSourcseDet(int VitSourceType, string NewVitaSource,int VitaminId)
        {
            try
            {
                if (VitSourceType == 1)
                {
                    FoodDetail ObjfoodDetail = new FoodDetail { FoodSource = NewVitaSource,VitaminId= VitaminId,IsFoodSourceDelete=false };
                    ObjEntity.FoodDetails.Add(ObjfoodDetail);
                    ObjEntity.SaveChanges();

                    return "Vitamin source added successfully..";
                }
                else
                {
                    FluidVitaminDetail ObjfluidVitaminDetail = new FluidVitaminDetail { VitaminId = VitaminId,FluidSource = NewVitaSource,IsFluidSourceDelete=false };
                    ObjEntity.FluidVitaminDetails.Add(ObjfluidVitaminDetail);
                    ObjEntity.SaveChanges();

                    return "Vitamin source added successfully..";
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public object GetVitaminSourceList(int VitSourceType, int VitaminId)
        {
            try
            {
                if(VitSourceType == 1)
                {
                    var Result = (from A in ObjEntity.FoodDetails.Where(a=>a.VitaminId == VitaminId && a.IsFoodSourceDelete==false)
                                  from B in ObjEntity.VitaminDetails.Where(b=>b.VitaminId == A.VitaminId)                                  
                                  select new {VitSourceId = A.FoodId,VitaminSource = A.FoodSource,A.VitaminId,B.Vitamin}).ToList();
                    return Result;
                }
                else
                {
                    var Result = (from A in ObjEntity.FluidVitaminDetails.Where(a=>a.VitaminId == VitaminId && a.IsFluidSourceDelete == false)
                                  from B in ObjEntity.VitaminDetails.Where(b => b.VitaminId == A.VitaminId)
                                  select new { VitSourceId = A.VitaminFluidId, VitaminSource= A.FluidSource, A.VitaminId, B.Vitamin }).ToList();
                    return Result;
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        public object EditVItaminSource(int VItaSourceTypeId, int VitSourceId, int EDVitaminId, string EditedFoodSource)
        {
            try
            {
                if (VItaSourceTypeId == 1)
                {
                    FoodDetail objfooddetail = ObjEntity.FoodDetails.Single(a => a.FoodId == VitSourceId);
                    objfooddetail.FoodSource = EditedFoodSource;
                    objfooddetail.VitaminId = EDVitaminId;
                    ObjEntity.SaveChanges();

                    return "Food Source Updated Successfully... ";

                }
                else
                {
                    FluidVitaminDetail Objfluidvitamindetail = ObjEntity.FluidVitaminDetails.Single(b => b.VitaminFluidId == VitSourceId);
                    Objfluidvitamindetail.VitaminId = EDVitaminId;
                    Objfluidvitamindetail.FluidSource = EditedFoodSource;
                    ObjEntity.SaveChanges();

                    return "Fluid Source Updated Successfully... ";

                }

            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public object DeleteFoodSource(int DeleteVItaSourceTypeId, int DeleteVitSourceId)
        {
            try
            { 
                if (DeleteVItaSourceTypeId == 1)
                {
                    FoodDetail ObjFD = ObjEntity.FoodDetails.Single(a => a.FoodId == DeleteVitSourceId);
                    ObjFD.IsFoodSourceDelete = true;
                    ObjEntity.SaveChanges();

                    return "Deleted successfully...";

                }
                else
                {
                    FluidVitaminDetail ObjFVD = ObjEntity.FluidVitaminDetails.Single(a => a.VitaminFluidId == DeleteVitSourceId);
                    ObjFVD.IsFluidSourceDelete = true;
                    ObjEntity.SaveChanges();

                    return "Deleted successfully...";
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

    }
}