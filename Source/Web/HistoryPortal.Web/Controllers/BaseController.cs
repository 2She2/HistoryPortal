using HistoryPortal.Data;
using HistoryPortal.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace HistoryPortal.Web.Controllers
{
    public class BaseController : Controller
    {
        private IHistoryPortalData data;
        private readonly ISanitizer sanitizer;

        public BaseController(IHistoryPortalData data, ISanitizer sanitizer)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            this.data = data;
            this.sanitizer = sanitizer;
        }

        public IHistoryPortalData Data
        {
            get { return this.data; }
        }

        public ISanitizer Sanitizer
        {
            get { return this.sanitizer; }
        }
    }
}