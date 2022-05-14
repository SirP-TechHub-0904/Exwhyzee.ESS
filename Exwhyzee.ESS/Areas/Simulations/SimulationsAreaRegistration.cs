using System.Web.Mvc;

namespace Exwhyzee.ESS.Areas.Simulations
{
    public class SimulationsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Simulations";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Simulations_default",
                "Simulations/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}