using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Estates.Models
{
    public class Tag
    {
        [Required, StringLength(40)]
        public string TagId { get; set; }

        [Required, StringLength(40)]
        public string TageName { get; set; }

        //Relation

        public virtual Item Item { get; set; }
        public string ItemId { get; set; }
    }
}