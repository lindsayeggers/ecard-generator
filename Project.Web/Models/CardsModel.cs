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

        [Required(ErrorMessage = "*Required")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid email")]
        [Display(Name = "Recipient Email")]
        public string ToEmail { get; set; }

        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Recipient Name")]
        public string ToName { get; set; }

        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Message")]
        public string Message { get; set; }

        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Your Name")]
        public string FromName { get; set; }

        [Required(ErrorMessage = "*Required")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid Email")]
        [Display(Name = "Your Email")]
        public string FromEmail { get; set; }

    }
}