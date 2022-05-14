using Exwhyzee.ESS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models
{
    public class QuestionMain
    {
        public int Id { get; set; }
        public int Sort { get; set; }
        public string Img { get; set; }
        public string TextQ { get; set; }
        public Question UserType { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string Answer { get; set; }

        public int QuestionCategoryId { get; set; }
        public QuestionCategory QuestionCategory { get; set; }

    }
}