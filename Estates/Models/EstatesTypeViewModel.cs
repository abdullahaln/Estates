using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Estates.Models
{
    public class EstatesTypeViewModel
    {
        [Required, StringLength(25)]
        public string TypeName { get; set; }

        [Required, StringLength(25)]
        public string  ItemId { get; set; }
    }
}