using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Estates.Models
{
    public class EstatesType
    {
        [Required, StringLength(40)]
        public string EstatesTypeId { get; set; }

        [Required, StringLength(25)]
        public string EstatesTypeName { get; set; }

        public DateTime AddedDate { get; set; }

        //Relations

        public virtual List<Item> Items { get; set; }
    }
}