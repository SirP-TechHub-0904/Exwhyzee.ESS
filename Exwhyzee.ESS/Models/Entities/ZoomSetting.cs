using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class ZoomSetting
    {
        public int Id { get; set; }
        public string ZoomHostEmail { get; set; }

        public string ZoomHostOne { get; set; }
        public string ZoomHostOnePass { get; set; }
        public string ZoomHostTwo { get; set; }
        public string ZoomHostTwoPass { get; set; }
        public string ZoomHostThree { get; set; }
        public string ZoomHostThreePass { get; set; }
        public bool EnableZoom { get; set; }
        public bool EnableMultipleZoom { get; set; }

        [Display(Name ="Monthly Subscription")]
        public decimal? SubscriptionAmount { get; set; }

    }
}