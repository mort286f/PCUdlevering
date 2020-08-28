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

        public ActionResult ReturnLendDate(string lend)
        {
            ComputerModel.SetLendDate(lend);
            return RedirectToAction("ListComputer");
        }

        public ActionResult ReturnHandInDate(string hand)
        {
            ComputerModel.SetHandInDate(hand);
            return RedirectToAction("ListComputer");
        }
    }
}