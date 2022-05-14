using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{
    public class SMSSettings
    {
        //Sms Settings
        public int Id { get; set; }

        [Display(Name = "SMS Username")]
        public string SmsUsername { get; set; }

        [Display(Name = "SMS Password")]
        public string SmsPassword { get; set; }
    }
}