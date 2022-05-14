﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exwhyzee.ESS.Models.Entities
{
    public class Game
    {
        public int Id { get; set; }
        [AllowHtml]
        public string IframLink { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Instruction { get; set; }
        public bool IsActive { get; set; }
        public DateTime Date { get; set; }

        public int CategoryGameId { get; set; }
        public CatogoryGame CatogoryGame { get; set; }
    }
}