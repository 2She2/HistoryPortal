using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HistoryPortal.Web.Areas.User.InputModels.Events
{
    public class EventsInputModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [DataType("tinymce_full")]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}