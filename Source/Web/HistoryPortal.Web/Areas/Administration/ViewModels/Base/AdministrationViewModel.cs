using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HistoryPortal.Web.Areas.Administration.ViewModels.Base
{
    public class AdministrationViewModel
    {
        public DateTime CreatedOn { get; set; }

        //[HiddenInput(DisplayValue = false)]
        public DateTime? ModifiedOn { get; set; }
    }
}