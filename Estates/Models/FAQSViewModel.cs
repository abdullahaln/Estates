using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Estates.Models
{
    public class FAQSViewModel
    {
        [Required,StringLength(512)]
        public string  Question { get; set; }

        [Required, StringLength(512)]
        public string  Answer { get; set; }

        public string UserId { get; set; }

    }
}