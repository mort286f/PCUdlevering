using PCUdlevering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCUdlevering.Controllers
{
    public class ComputerController : Controller
    {
        // GET: Computer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListComputer()
        {
            return View(ComputerModel.GetComputers());
        }

        public ActionResult ReturnLendDate(string pcid)
        {
            ComputerModel.SetLendDate(pcid);
            return RedirectToAction("ListComputer");
        }

        public ActionResult ReturnHandInDate()
        {
            return View();
        }
        public ActionResult Delete(string pc)
        {

            return RedirectToAction("ListComputer");
        }
    }
}