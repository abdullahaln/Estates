using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Estates.Models
{
    public class ItemImage
    {
        [Required, StringLength(40)]
        public string ItemImageId { get; set; }

        [Required, StringLength(64)]
        public string Imagepath { get; set; }

        public DateTime AddedDate { get; set; }

        //Relation

        public Item Item { get; set; }
        public string ItemId { get; set; }

    }
}