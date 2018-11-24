using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Estates.Models
{
    public class Review
    {

        [Required, StringLength(40)]
        public string ReviewId { get; set; }

        [Required, StringLength(30)]
        public string NickName { get; set; }

        [Required, StringLength(512)]
        public string Description { get; set; }

        public double Value { get; set; }

        [Required, StringLength(40)]
        public string Titel { get; set; }

        [Required, StringLength(40)]
        public string IpAddress { get; set; }

        public DateTime ReviewDate { get; set; }

        //Relations

        public virtual Person Person { get; set; }
        public string PersonId { get; set; }

    }
}