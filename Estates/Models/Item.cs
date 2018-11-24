using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Estates.Models
{
    public class Item
    {
        [Required, StringLength(40)]
        public string ItemId { get; set; }

        [Required, StringLength(512)]
        public string Description { get; set; }

        [Required, StringLength(40)]
        public string Title { get; set; }

        public decimal Price { get; set; }

        [Required, StringLength(25)]
        public string ItemType { get; set; }

        [Required, StringLength(64)]
        public string MainImagePath { get; set; }

        [Required, StringLength(40)]
        public string IpAddress { get; set; }

        public bool IsHidden { get; set; }

        public bool IsSold { get; set; }

        public DateTime AddedDate { get; set; }

        //Relation
        public virtual Customer Customer { get; set; }
        public string CustomerId { get; set; }

        public virtual EstatesType EstatesType { get; set; }
        public string EstatesTypeId { get; set; }

        public virtual List<ItemImage> ItemImages { get; set; }

        public virtual List<Tag> Tags { get; set; }
    }
}