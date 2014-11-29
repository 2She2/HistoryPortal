using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace HistoryPortal.Web.InputModels.Comments
{
    public class CommentInputModel
    {
        [Required]
        public int ArticleId { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, MinimumLength = 2)]
        [AllowHtml]
        public string Content { get; set; }
    }
}