using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Estates.Models
{
    public class CustomerViewModel
    {
        [Required,StringLength(25)]
        public string  FirstName { get; set; }

        [Required,StringLength(25)]
        public string LastName { get; set; }

        [Required,StringLength(40)]
        public string Email { get; set; }

        [Required,StringLength(25)]
        public string phone { get; set; }

        [Required,StringLength(64)]
        public string Address { get; set; }


    }
}