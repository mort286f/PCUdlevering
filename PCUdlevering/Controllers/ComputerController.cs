using PCUdlevering.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCUdlevering.Controllers
{
    public class ComputerController : Controller
    {
        public ActionResult ListComputer()
        {
            return View(ComputerModel.GetComputers());
        }

        [ChildActionOnly]
        public ActionResult ReturnLendDate(string lend)
        {
            ComputerModel.SetLendDate(lend);
            ComputerModel.GetComputers();
            return PartialView("_DateUpdate");
        }

        public ActionResult ReturnHandInDate(string hand)
        {
            ComputerModel.SetHandInDate(hand);
            ComputerModel.GetComputers();
            return View(ComputerModel.GetComputers());
        }

        public ActionResult OpenPopup()
        {
            return View("_LoginScreen");
        }

        protected string renderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName)) 
                viewName = ControllerContext.RouteData.GetRequiredString("action");
            ViewData.Model = model;
            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewresult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewcontext = new ViewContext(ControllerContext, viewresult.View, ViewData, TempData, sw);
                viewresult.View.Render(viewcontext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        public ActionResult ViewToJsonAction(string lend, string hand)
        {
            if (lend != null)
            {
                ComputerModel.SetLendDate(lend);
            }
            else
            {
                ComputerModel.SetHandInDate(hand);
            }
            var computerlist = ComputerModel.GetComputers();
            var jsonmodel = renderPartialViewToString("_ComputerTable", computerlist);
            return Json(jsonmodel);
        }

        public ActionResult ComputerList(Models.DAOClasses.DAOComputer dAOComputer)
        {
            return PartialView(dAOComputer);
        }
    }
}