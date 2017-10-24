using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class CardsModel
    {
        public int CardId { get; set; }
        public string CardName { get; set; }
        public string ImageName { get; set; }
        public string FontColor { get; set; }

        public int Id { get; set; }
        public int TemplateId { get; set; }

        [Display(Name = "Recipient Email")]
        public string ToEmail { get; set; }

        [Display(Name = "Recipient Name")]
        public string ToName { get; set; }

        [Display(Name = "Message")]
        public string Message { get; set; }

        [Display(Name = "Your Name")]
        public string FromName { get; set; }

        [Display(Name = "Your Email")]
        public string FromEmail { get; set; }

    }
}