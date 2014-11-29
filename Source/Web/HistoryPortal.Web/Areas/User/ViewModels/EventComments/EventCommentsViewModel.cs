using HistoryPortal.Data.Models;
using HistoryPortal.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HistoryPortal.Web.Areas.User.Views.EventComments
{
    public class EventCommentsViewModel : IMapFrom<EventComment>
    {
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}