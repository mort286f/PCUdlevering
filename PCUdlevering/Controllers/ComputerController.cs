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

        public ActionResult ReturnLendDate()
        {
            //er ikke done her
            return View(ComputerModel.SetLendDate());
        }

        public ActionResult ReturnHandInDate()
        {
            return View();
        }
    }
}