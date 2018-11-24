using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Estates.Models
{
    public class ItemViewModel
    {
        [Required, StringLength(40)]
        public string  Title { get; set; }

        [Required, StringLength(512)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        [Required, StringLength(64)]
        public string MainImagePath { get; set; }

        [Required, StringLength(25)]
        public string ItemType { get; set; }

        [Required, StringLength(50)]
        public string TypeId { get; set; }

        [Required, StringLength(50)]
        public string CustomerId { get; set; }

    }
}