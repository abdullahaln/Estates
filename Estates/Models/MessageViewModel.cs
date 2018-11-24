using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Estates.Models
{
    public class MessageViewModel
    {
        [Required, StringLength(40)]
        public string FromId { get; set; }

        [Required, StringLength(40)]
        public string ToId { get; set; }

        [Required, StringLength(512)]
        public string Description { get; set; }
    }
}