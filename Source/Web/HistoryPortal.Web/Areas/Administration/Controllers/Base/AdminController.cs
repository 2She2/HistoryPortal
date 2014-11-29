namespace HistoryPortal.Web.Areas.Administration.Controllers.Base
{
    using HistoryPortal.Data;
    using HistoryPortal.Web.Controllers;
    using HistoryPortal.Web.Infrastructure;
    using System.Globalization;
    using System.Threading;
    using System.Web.Mvc;


     [Authorize(Roles = "admin")]
    public abstract class AdminController : BaseController
    {
        public AdminController(IHistoryPortalData data, ISanitizer sanitizer)
            : base(data, sanitizer)
        {
        }
    }
}