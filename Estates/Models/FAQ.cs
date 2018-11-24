using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Estates.Models
{
    public class FAQ
    {
        [Required, StringLength(40)]
        public string FAQId { get; set; }

        [Required, StringLength(512)]
        public string Question { get; set; }

        [Required, StringLength(40)]
        public string UserId { get; set; }

        [Required, StringLength(512)]
        public string Answer { get; set; }
    }
}