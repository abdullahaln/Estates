using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Estates.Models
{
    [Table("Customer")]
    public class Customer : Person
    {
        [Required, StringLength(64)]
        public string Address { get; set; }

        [Required, StringLength(30)]
        public string Phone { get; set; }

        public bool IsActive { get; set; }

        //Relation

        public List<Item> Items { get; set; }

    }
}