using Project.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers
{
    public class HomeController : Controller
    {
        private IECardDAL dal;
        public HomeController(IECardDAL dal)
        {
            this.dal = dal;
        }

        // GET: Home
        public ActionResult Index()
        {
            var cards = dal.GetAllCards();

            return View("Index", cards);
        }

       
    }
}