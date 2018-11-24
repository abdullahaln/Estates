using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Estates.Models
{
    public class Message
    {
        [Required, StringLength(40)]
        public string MessageId { get; set; }

        [Required, StringLength(40)]
        public string FromId { get; set; }

        [Required, StringLength(40)]
        public string ToId { get; set; }

        [Required, StringLength(512)]
        public string Description { get; set; }

        public DateTime MessageDate { get; set; }

        public bool IsRead { get; set; }

        //Relation

        public Person person { get; set; }
        public string PersonId { get; set; }
    }
}