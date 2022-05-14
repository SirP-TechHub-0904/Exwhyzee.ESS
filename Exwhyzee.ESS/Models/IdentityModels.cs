using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Exwhyzee.ESS.Models.Entities;
using Exwhyzee.ESS.Models.ProfileDegreeObtained;
using Exwhyzee.ESS.Models.ProfileEducationHistory;
using Exwhyzee.ESS.Models.ProfileExperience;
using Exwhyzee.ESS.Models.ProfileInterpersonalSkill;
using Exwhyzee.ESS.Models.ProfileJobAnalysis;
using Exwhyzee.ESS.Models.ProfileMembershipOfLearneredSociety;
using Exwhyzee.ESS.Models.ProfileMeritCertificate;
using Exwhyzee.ESS.Models.ProfileReferee;
using Exwhyzee.ESS.Models.ProfileRenderedService;
using Exwhyzee.ESS.Models.ProfileSkill;
using Exwhyzee.ESS.Models.ProfileWorkHistory;
using System.Drawing;
using Exwhyzee.ESS.Models.UserProfile;
using Exwhyzee.ESS.Models.Profile;
using System;
using System.ComponentModel.DataAnnotations;

namespace Exwhyzee.ESS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        [Display(Name = "Surname")]
        public string SurName { get; set; }
        [Display(Name = "Firstname")]
        public string FirstName { get; set; }
        [Display(Name = "Lastname")]
        public string LastName { get; set; }
        public string School { get; set; }
        public DateTime? LastSeen { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<SchoolPortalData> SchoolPortalDatas { get; set; }
        public DbSet<SessionAnalysis> SessionAnalyses { get; set; }
        public DbSet<TermlyAnalysis> TermlyAnalyses { get; set; }
        public DbSet<NPSTschools> NPSTschools { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<CatogoryGame> CatogoryGames { get; set; }
        public DbSet<Game> Gamess { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Simulation> Simulations { get; set; }
        public DbSet<SimulationCategory> SimulationCategories { get; set; }
        public DbSet<SimulationSubject> SimulationSubjects { get; set; }
        public DbSet<ClassLevel> ClassLevels { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Question> Questions { get; set; }

        public DbSet<TopicReview> TopicReview { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<SchoolRanking> SchoolRankings { get; set; }
        public DbSet<QuestionCategory> QuestionCategories { get; set; }
        public DbSet<QuestionMain> QuestionMain { get; set; }
        public DbSet<Participant> Participants { get; set; }   
        public DbSet<ParticipantResult> ParticipantResults { get; set; }

        public DbSet<Testimony> Testimonies { get; set; }
        public DbSet<DegreeObtainedModel> DegreeObtainedModels { get; set; }
        public DbSet<EducationHistoryModel> EducationHistoryModels { get; set; }
        public DbSet<ExperienceModel> ExperienceModels { get; set; }
        public DbSet<InterpersonalSkillModel> InterpersonalSkillModels { get; set; }
        public DbSet<JobAnalysisModel> JobAnalysisModels { get; set; }
        public DbSet<LanguageSpokenModel> LanguageSpokenModels { get; set; }
        public DbSet<MembershipOfLearneredSocietyModel> MembershipOfLearneredSocietyModels { get; set; }
        public DbSet<MeritCertificateModel> MeritCertificateModels { get; set; }
        public DbSet<RefereeModel> RefereeModels { get; set; }
        public DbSet<RenderedServiceModel> RenderedServiceModels { get; set; }
        public DbSet<SkillModel> SkillModels { get; set; }
        public DbSet<TrainingAndWorkShopModel> TrainingAndWorkShopModels { get; set; }
        public DbSet<WorkHistoryModel> WorkHistoryModels { get; set; }
        public DbSet<DataImages> DataImages { get; set; }
        public DbSet<UserProfileModel> UserProfileModels { get; set; }
        public DbSet<UserReview> UserReviews { get; set; }
        public DbSet<SubjectSpecialistModel> SubjectSpecialistModels { get; set; }
        public DbSet<ImageSlider> ImageSliders { get; set; }
        public DbSet<States> States { get; set; }
        public DbSet<LocalGovs> LocalGovs { get; set; }
        public DbSet<StudentModel> StudentModel { get; set; }
        public DbSet<LiveClassLevel> LiveClassLevel { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<LiveClassOnline> LiveClassOnlines { get; set; }

        public DbSet<OnlineZoom> OnlineZooms { get; set; }

        public DbSet<ZoomSetting> ZoomSetting { get; set; }

        public DbSet<ZoomStudentJoinedRecord> ZoomStudentJoinedRecord { get; set; }

        public DbSet<LiveClassSubject> LiveClassSubject { get; set; }
        public DbSet<LiveClassSubjectEnrollment> LiveClassSubjectEnrollment { get; set; }
        public DbSet<SmsReport> SmsReports { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<TeacherLiveSubjectAssignment> TeacherLiveSubjectAssignments { get; set; }
        public DbSet<TeacherWallet> TeacherWallet { get; set; }
        public DbSet<TeacherWithdrawal> TeacherWithdrawal { get; set; }
        public DbSet<SMSSettings> SMSSettings { get; set; }
        public DbSet<SubscriptionCommision> SubscriptionCommision { get; set; }
        public DbSet<TeacherEvaluation> TeacherEvaluation { get; set; }
        public DbSet<SchoolSession> SchoolSessions { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}