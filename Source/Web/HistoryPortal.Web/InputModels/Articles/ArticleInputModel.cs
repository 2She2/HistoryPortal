using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace HistoryPortal.Web.InputModels.Articles
{
    public class ArticleInputModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [DataType("tinymce_full")]
        public string Content { get; set; }

        [Required]
        public string Category { get; set; }
    }
}