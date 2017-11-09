using Project.Web.DAL;
using Project.Web.DeliveryProviders;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

            CardsModel template = dal.GetATemplate(c.TemplateId);
            

            //creating a image object
            System.Drawing.Image bitmap = (System.Drawing.Image)Bitmap.FromFile(Server.MapPath($"~/Content/img/{template.ImageName}")); // set image 
                                                                                                             
            Graphics graphicsImage = Graphics.FromImage(bitmap);

            //Set the alignment based on the coordinates   
            StringFormat stringformat = new StringFormat();
            stringformat.Alignment = StringAlignment.Far;
            stringformat.LineAlignment = StringAlignment.Far;


            //Set the font color/format/size etc..  
            Color StringColor = System.Drawing.ColorTranslator.FromHtml("#933eea");//direct color adding
            string Str_TextOnImage = c.Message;//Your Text On Image

            graphicsImage.DrawString(Str_TextOnImage, new Font("arial", 40,
            FontStyle.Regular), new SolidBrush(StringColor), new Point(268, 245),
            stringformat); 


            var randomFileName = Guid.NewGuid().ToString() + ".jpg";
            var finalFilePath = Server.MapPath($"~/Content/img/{randomFileName}");
            bitmap.Save(finalFilePath);

            deliveryService.Send(c.ToEmail, finalFilePath);

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