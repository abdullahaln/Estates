using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Estates.Models
{
    public class SliderImage
    {
        [Required, StringLength(40)]
        public string SliderImageId { get; set; }

        [Required, StringLength(64)]
        public string ImagePath { get; set; }

        [Required, StringLength(64)]
        public string URL { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}