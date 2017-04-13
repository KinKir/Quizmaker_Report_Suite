using System.Web;
using System.Web.Mvc;

namespace Quizmaker_Report_Suite
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
