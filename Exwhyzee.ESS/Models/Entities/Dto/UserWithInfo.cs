﻿using Exwhyzee.ESS.Models.Profile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities.Dto
{
    public class UserWithInfo
    {
        public long Id { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Surname")]
        public string SurName { get; set; }
        [Display(Name = "Firstname")]
        public string FirstName { get; set; }
        [Display(Name = "Lastname")]
        public string LastName { get; set; }
        [Display(Name = "Contact Address")]
        public string ContactAddress { get; set; }
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }
        public DateTime DateRegistered { get; set; }
        public string UserId { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string LinkedinLink { get; set; }
        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "LGA")]
        public string LGA { get; set; }

        [Display(Name = "Religion")]
        public string Religion { get; set; }
        public byte[] PhotoUrl { get; set; }
        public string ComplementryCardKeywords { get; set; }
        public string IskoolId { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string AdminReview { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ApprovalAdmin AdminApproval { get; set; }
        public int CV_Views { get; set; }
        public int Portfolio_Views { get; set; }
        public bool Verified { get; set; }

        public string LastSeen { get; set; }
        public string SubjectString { get; set; }

        public ApplicationUser User { get; set; }

        public List<Topic> Topics { get; set; }

        public List<UserReview> Reviews { get; set; }
        public List<SubjectSpecialistModel> SubjectSpecialistModels { get; set; }
    }
}