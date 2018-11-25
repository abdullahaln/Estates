using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Estates.Models
{
    public abstract class Person
    {
        [Required, StringLength(40)]
        public string Id { get; set; }

        [Required, StringLength(25)]
        public string FirstName { get; set; }

        [Required, StringLength(25)]
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }


        [Required, StringLength(40)]
        public string IpAddress { get; set; }

        [Required, StringLength(40)]
        public string Email { get; set; }

        public bool IsBlocked { get; set; }

        //Relations
        public virtual List<Message> Messages { get; set; }

        public virtual List<Review> Reviews { get; set; }

    }
}