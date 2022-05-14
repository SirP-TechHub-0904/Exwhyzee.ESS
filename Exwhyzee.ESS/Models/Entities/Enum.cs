using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Exwhyzee.ESS.Models.Entities
{


    public enum LiveStatus
    {

        [Description("Active")]
        Active = 1,
        [Description("Ended")]
        Ended = 2,
        [Description("Waiting")]
        Waiting = 3,

    }

    public enum WithdrawalStatus
    {
        [Description("None")]
        None = 0,
        [Description("Pending")]
        Pending = 1,

        [Description("Approved")]
        Approved = 2
    }

    public enum SubscriptionSource
    {
        [Description("None")]
        None = 0,
        [Description("Deposit")]
        Cash = 1,

        [Description("Transfer")]
        Bank = 2,
        [Description("Online")]
        Online = 3
    }

    public enum SubscriptionStatus
    {
        [Description("None")]
        None = 0,

        [Description("Cancel")]
        Cancel = 1,

        [Description("Pending")]
        Pending = 2,

        [Description("Paid")]
        Paid = 3

    }
    public enum ApprovalAdmin
    {
        [Description("Approved")]
        Approved = 1,
        [Description("Not Approved")]
        NotApproved = 2,

        [Description("Pending")]
        Pending = 0
    }

    public enum SessionStatus
    {
        [Description("Not Used")]
        NotUsed = 1,

        [Description("Used")]
        Used = 2,

        [Description("Current")]
        Current = 3
    }

    public enum QuestionEnum
    {
        [Description("Use Text")]
        UseText = 1,

        [Description("Use Img")]
        UseImg = 2
    }

    public enum School
    {
        [Description("KindergartenAndNursery")]
        KindergartenAndNursery = 1,

        [Description("Primary")]
        Primary = 3,
        [Description("Secondary")]
        Secondary = 4,
        [Description("Motivation")]
        Motivation = 5
    }

    public enum SchoolType
    {
        [Description("Nursery & Primary")]
        NP = 1,

        [Description("Secondary")]
        Secondary = 2,

        [Description("Tertiary")]
        Tertiary = 3
    }

    public enum PostStatus
    {
        [Description("Published")]
        Published = 1,

        [Description("Deleted")]
        Deleted = 2
    }

    public enum WhoSeePost
    {
        [Description("All")]
        All = 0,
        [Description("Primary")]
        Student = 1,

        [Description("Secondary")]
        Staff = 2
    }

}