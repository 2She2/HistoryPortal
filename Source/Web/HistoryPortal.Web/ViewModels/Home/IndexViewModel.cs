namespace HistoryPortal.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<IndexArticleViewModel> NewestArticles { get; set; }

        public IEnumerable<IndexArticleViewModel> PopularArticles { get; set; }
    }
}