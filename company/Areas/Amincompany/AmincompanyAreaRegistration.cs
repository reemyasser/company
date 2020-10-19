using System.Web.Mvc;

namespace company.Areas.Amincompany
{
    public class AmincompanyAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Amincompany";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Amincompany_default",
                "Amincompany/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}