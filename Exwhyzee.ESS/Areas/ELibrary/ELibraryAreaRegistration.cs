using System.Web.Mvc;

namespace Exwhyzee.ESS.Areas.ELibrary
{
    public class ELibraryAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ELibrary";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ELibrary_default",
                "ELibrary/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}