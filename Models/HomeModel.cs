using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using VutaCheck01.Entity;

namespace VutaCheck01.Models
{
    public class HomeModel
    {
        VitaCheck_DBEntities ObjEntity = new VitaCheck_DBEntities();
        public object GetSymptomList()
        {
            var a = (from B in ObjEntity.Symptoms.Where(x => x.IsSymptomDelete == false)
                     select new { B.SymptomDescription, B.SymptomId }).ToList();

            return a;

        }
        public object GetDeficentVitaminDetail(string SelectedSymptomId)
        {
            try
            {
                string[] SelectedSymptomIds = SelectedSymptomId.Split(',');
                List<int> Ids = new List<int>();

                foreach (string SymptomId in SelectedSymptomIds)
                {
                    if (int.TryParse(SymptomId, out int Id))
                    {
                        Ids.Add(Id);

                    }
                }
                var result = (from A in ObjEntity.Symptoms.Where(aa => Ids.Contains(aa.SymptomId) && aa.IsSymptomDelete == false)
                              from B in ObjEntity.VitaminSymptomRelations.Where(b => b.SymptomId == A.SymptomId && b.IsRelationDelete == false)
                              from C in ObjEntity.VitaminDetails.Where(c => c.VitaminId == B.VitaminId)
                              select new { A.SymptomId, C.VitaminId, C.Vitamin, A.SymptomDescription, C.VitaminDescription }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public object GetNutritiousfood(int VitaminId)
        {
            try
            {
                var FoodResult = (from A in ObjEntity.VitaminDetails.Where(a => a.VitaminId == VitaminId)
                                  from B in ObjEntity.FoodDetails.Where(b => b.VitaminId == A.VitaminId && b.IsFoodSourceDelete == false)
                                  select new { B.FoodSource }).ToList();

                var FluidResult = (from A in ObjEntity.VitaminDetails.Where(n => n.VitaminId == VitaminId)
                                   from B in ObjEntity.FluidVitaminDetails.Where(m => m.VitaminId == VitaminId && m.IsFluidSourceDelete == false)
                                   select new { B.FluidSource }).ToList();
                return new { FoodResult, FluidResult };

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public object JoinNewUser(string FirstName, string LastName, string Email, string UserName, string Password)
        {

            try
            {
                UserDetail objUserDetails = new UserDetail { FirstName = FirstName, LastName = LastName,
                                                        Email = Email, UserName = UserName, Password = Password, IsUser = true };
                ObjEntity.UserDetails.Add(objUserDetails);
                ObjEntity.SaveChanges();
                return "Registration completd Successfully";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public object CheckUserDetails(string UserName, string Password)
        {
            try
            {

                if (ObjEntity.UserDetails.Any(x => x.UserName == UserName && x.Password == Password))
                {
                    bool? UserStatus = false;
                    UserStatus = (from A in ObjEntity.UserDetails.Where(x => x.UserName == UserName && x.Password == Password) select A.IsUser).Single();

                    var UserDetails = (from A in ObjEntity.UserDetails.Where(x => x.UserName == UserName && x.Password == Password)
                                       select new { Name = A.FirstName + " " + A.LastName, A.UserId }).Single();
                    return new { UserStatus, UserDetails };
                }
                else
                {
                    return "Incorrect Username and Password..";
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //public object GetUserDetails(string UserName, string Password)
        //{
        //    try
        //    {
        //        if (ObjEntity.UserDetails.Any(x => x.UserName == UserName && x.Password == Password))
        //        {                    
        //            var Data = (from A in ObjEntity.UserDetails.Where(x => x.UserName == UserName && x.Password == Password) select new { Name = A.FirstName + " " + A.LastName }).Single();
        //            return Data;
        //        }
        //        else
        //        {
        //            return "Incorrect Username and Password..";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        public object GetEducationalContent()
        {
            try
            {
                var VitaminFact = (from A in ObjEntity.VitaminFactDetails.Where(s => s.IsVitaFactDelete == false) select new { A.VitaminFact }).ToList();

                return VitaminFact;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public object GetFoodList(int SourceType)
        {
            if (SourceType == 1)
            {
                var Data = (from A in ObjEntity.FoodDetails.Where(a => a.IsFoodSourceDelete == false)
                            select new
                            {
                                SourceIntakeId = A.FoodId,
                                SourceName = A.FoodSource,
                                A.VitaminId
                            }).ToList();
                var distinctData = Data.GroupBy(a => a.SourceName).Select(group => new
                {
                    SourceName = group.Key,
                    SourceIntakeId = group.First().SourceIntakeId
                }).OrderBy(o => o.SourceName).ToList();
                return distinctData;
            }
            else
            {
                var Data = (from A in ObjEntity.FluidVitaminDetails.Where(a => a.IsFluidSourceDelete == false)
                            select new { SourceIntakeId = A.VitaminFluidId, SourceName = A.FluidSource, A.VitaminId }).ToList();
                return Data;
            }
        }
        public object LogFoodIntakeDetails(string IntakeDate, string IntakeTime, int SourceType, string AdditionalInfo, string SelectedSource, int UserId)
        {
            try
            {
                DateTime intakedate = DateTime.ParseExact(IntakeDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                string[] SelectedSourceId = SelectedSource.Split(',');
                List<int> FoodIds = new List<int>();

                foreach (string FoodId in SelectedSourceId)
                {
                    if (int.TryParse(FoodId, out int Id))
                    {
                        FoodIds.Add(Id);
                    }
                }
                if (SourceType == 1)
                {
                    foreach (int FoodId in FoodIds)
                    {
                        FoodIntakeDetail ObjFoodIntakeDetail = new FoodIntakeDetail
                        {
                            FoodId = FoodId,
                            FoodIntakeDate = intakedate,
                            FoodIntakeTime = IntakeTime,
                            AdditionalInfo = AdditionalInfo,
                            UserId = UserId 
                            
                        };
                        ObjEntity.FoodIntakeDetails.Add(ObjFoodIntakeDetail); 
                    }

                    ObjEntity.SaveChanges();
                    return "Food Intake Recorded Successfully!";
                }
                else
                {
                    foreach (int FoodId in FoodIds)
                    {
                        FluidIntakeDetail ObjFluidIntakeDetail = new FluidIntakeDetail
                        {
                            VitaminFluidId = FoodId,
                            FluidIntakeDate = intakedate,
                            FluidIntakeTime = IntakeTime,
                            AdditionalInfo = AdditionalInfo,
                            UserId= UserId
                        };
                        ObjEntity.FluidIntakeDetails.Add(ObjFluidIntakeDetail);
                    }

                    ObjEntity.SaveChanges();
                    return "Fluid Intake Recorded Successfully!";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            } 
        }
        public object GetFoodIntakeHistory(string FromDate, string ToDate, int SourceType, int UserId)
        {
            try
            { 
                DateTime fromDate = DateTime.Parse(FromDate);
                DateTime toDate = DateTime.Parse(ToDate);
                if (SourceType == 1)
                {
                    var Data = (from A in ObjEntity.FoodIntakeDetails.Where(a => a.FoodIntakeDate >= fromDate && a.FoodIntakeDate <= toDate && a.UserId==UserId)
                                from B in ObjEntity.FoodDetails.Where(b => b.FoodId == A.FoodId && b.IsFoodSourceDelete == false)
                                from C in ObjEntity.VitaminDetails.Where(c => c.VitaminId == B.VitaminId)
                                select new
                                {
                                    IntakeId = A.FoodIntakeId,
                                    IntakeDate = A.FoodIntakeDate,
                                    IntakeTime = A.FoodIntakeTime,
                                    AdditionalInfo = A.AdditionalInfo == "" ? "-" : A.AdditionalInfo,
                                    Source = B.FoodSource,
                                    C.Vitamin,
                                    A.FoodId 
                                }).ToList();

                    var Result = Data.ConvertAll(Z => new
                    {
                        Z.IntakeId,
                        Date = Convert.ToDateTime(Z.IntakeDate).ToString("dd/mm/yyyy"),
                        Z.IntakeTime,
                        Z.AdditionalInfo,
                        Z.Source,
                        Z.Vitamin 
                    }).ToList(); 

                    return Result;
                }
                else
                {
                    var Data = (from A in ObjEntity.FluidIntakeDetails.Where(a =>a.FluidIntakeDate >= fromDate && a.FluidIntakeDate <= toDate && a.UserId == UserId)
                                from B in ObjEntity.FluidVitaminDetails.Where(b=>b.VitaminFluidId == A.VitaminFluidId
                                && b.IsFluidSourceDelete == false)
                                from C in ObjEntity.VitaminDetails.Where(c=>c.VitaminId == B.VitaminId)
                                select new
                                {
                                    IntakeId= A.FluidIntakeId,
                                    IntakeDate = A.FluidIntakeDate,                                   
                                    IntakeTime = A.FluidIntakeTime,
                                    A.AdditionalInfo,
                                    Source = B.FluidSource,
                                    C.Vitamin                                
                                }).ToList();

                    var Result = Data.ConvertAll(Z => new
                    {
                        Z.IntakeId,
                        Date = Convert.ToDateTime(Z.IntakeDate).ToString("dd/mm/yyyy"),
                        Z.IntakeTime,
                        Z.AdditionalInfo,
                        Z.Source,
                        Z.Vitamin
                    }).ToList();

                    return Result;

                }  
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

    }
}