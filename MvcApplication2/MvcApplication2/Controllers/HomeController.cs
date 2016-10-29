using MvcApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult GetAjaxData(JQueryDataTableParamModel param)
        
        {
            //using (var e = new ExampleEntities())
            //{
            //    var totalRowsCount = new System.Data.Objects.ObjectParameter("TotalRowsCount", typeof(int));
            //    var filteredRowsCount = new System.Data.Objects.ObjectParameter("FilteredRowsCount", typeof(int));

            //    var data = e.pr_SearchPerson(param.sSearch,
            //                                    Convert.ToInt32(Request["iSortCol_0"]),
            //                                    Request["sSortDir_0"],
            //                                    param.iDisplayStart,
            //                                    param.iDisplayStart + param.iDisplayLength,
            //                                    totalRowsCount,
            //                                    filteredRowsCount);

            //    var aaData = data.Select(d => new string[] { d.FirstName, d.LastName, d.Nationality, d.DateOfBirth.Value.ToString("dd MMM yyyy") }).ToArray();

            //    return Json(new
            //    {
            //        sEcho = param.sEcho,
            //        aaData = aaData,
            //        iTotalRecords = Convert.ToInt32(totalRowsCount.Value),
            //        iTotalDisplayRecords = Convert.ToInt32(filteredRowsCount.Value)
            //    }, JsonRequestBehavior.AllowGet);
            //}


            var totalRowsCount = new System.Data.Objects.ObjectParameter("TotalRowsCount", typeof(int));
            var filteredRowsCount = new System.Data.Objects.ObjectParameter("FilteredRowsCount", typeof(int));           
            DBClass dbclass = new DBClass();
            var data=  dbclass.GetEmployee(param.sSearch,
                                            Convert.ToInt32(Request["iSortCol_0"]),
                                            Request["sSortDir_0"],
                                            param.iDisplayStart,
                                            param.iDisplayStart + param.iDisplayLength);

           var aaData = data.Select(d => new string[] {d.ID.ToString(),d.Name,d.Age.ToString() }).ToArray();

            return Json(new
            {
                sEcho = param.sEcho,
                aaData = aaData,
                iTotalRecords = Convert.ToInt32(totalRowsCount.Value),
                iTotalDisplayRecords = Convert.ToInt32(filteredRowsCount.Value)
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
