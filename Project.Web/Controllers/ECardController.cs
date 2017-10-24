using Project.Web.DAL;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers
{
    public class ECardController : Controller
    {
        private IECardDAL dal;
        public ECardController(IECardDAL dal)
        {
            this.dal = dal;
        }

        // GET: ECard
        public ActionResult CreateCard(int id)
        {
            if (Session["CurrentCard"] != null)
            {
                CardsModel c = Session["CurrentCard"] as CardsModel;
                CardsModel template = dal.GetATemplate(c.TemplateId);
                c.ImageName = template.ImageName;

                return View("CreateCard", c);
            }
            else
            {
                CardsModel c = dal.GetATemplate(id);
                return View("CreateCard", c);
            }

        }

        [HttpPost]
        public ActionResult CreateCard(CardsModel c)
        {
            Session["CurrentCard"] = c;

            return RedirectToAction("ViewCard", c);
        }

        public ActionResult ViewCard(CardsModel c)
        {
            CardsModel template = dal.GetATemplate(c.TemplateId);
            c.ImageName = template.ImageName;

            return View("ViewCard", c);
        }

        [HttpPost]
        public ActionResult SaveCard(CardsModel c)
        {
            dal.SaveNewCard(c);

            Session["CurrentCard"] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}