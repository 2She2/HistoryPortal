namespace HistoryPortal.Web.InputModels.Articles
{
    using System.Collections.Generic;

    using HistoryPortal.Web.ViewModels;
    using System.Web.Mvc;

    public class CreateArticleInputModel
    {
        public ArticleInputModel InputModel { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}