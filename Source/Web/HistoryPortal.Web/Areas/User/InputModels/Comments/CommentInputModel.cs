using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HistoryPortal.Web.Areas.User.InputModels.Comments
{
    public class CommentInputModel
    {
        [Required]
        public int EventId { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, MinimumLength = 2)]
        [AllowHtml]
        public string Content { get; set; }
    }
}