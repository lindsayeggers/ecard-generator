using Project.Web.DAL;
using Project.Web.DeliveryProviders;
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
        private IDeliveryService deliveryService;

        public ECardController(IECardDAL dal, IDeliveryService deliveryService)
        {
            this.dal = dal;
            this.deliveryService = deliveryService;
        }

       

        // GET: ECard
        public ActionResult CreateCard(int id)
        {
                CardsModel c = dal.GetATemplate(id);
                return View("CreateCard", c);
        }

        public ActionResult EditCard(int id)
        {
            
            if (Session["CurrentCard"] != null)
            {
                CardsModel c = Session["CurrentCard"] as CardsModel;
                CardsModel template = dal.GetATemplate(c.TemplateId);
                c.ImageName = template.ImageName;

                return View("CreateCard", c);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult CreateCard(CardsModel c)
        {
            if (!ModelState.IsValid)
            {
                Session["CurrentCard"] = c;
                CardsModel template = dal.GetATemplate(c.TemplateId);
                c.ImageName = template.ImageName;
                return View("CreateCard", c);
            }
            else
            {
                Session["CurrentCard"] = c;

                return RedirectToAction("ViewCard", c);
            }
        }

        public ActionResult ViewCard(CardsModel c)
        {
            CardsModel template = dal.GetATemplate(c.TemplateId);
            c.ImageName = template.ImageName;

            return View("ViewCard", c);
        }

        [HttpPost]
        public ActionResult SaveCard()
        {
            CardsModel c = Session["CurrentCard"] as CardsModel;
            dal.SaveNewCard(c);

            deliveryService.Send(c.ToEmail, "C:\Users\Lindsay Eggers\Desktop\m3-w9-project-COPY\Project.Web\Content\img\3Trolls.jpg");

            Session["CurrentCard"] = null;

            return RedirectToAction("ThankYou");
        }

        public ActionResult ThankYou()
        {
            var cards = dal.GetAllCards();
            return View("ThankYou", cards);
        }


    }
}