using HistoryPortal.Data.Models;
using HistoryPortal.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HistoryPortal.Web.ViewModels.Home
{
    public class IndexArticleViewModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }


    }
}