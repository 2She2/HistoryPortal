using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using HistoryPortal.Web.InputModels.Comments;
using HistoryPortal.Data.Models;
using HistoryPortal.Data;
using HistoryPortal.Web.Infrastructure;
using HistoryPortal.Web.ViewModels.Comments;

namespace HistoryPortal.Web.Controllers
{
    public class CommentsController : BaseController
    {
        public CommentsController(IHistoryPortalData data, ISanitizer sanitizer)
            : base(data, sanitizer)
        {
        }

        // GET: Comments
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult PostComment(CommentInputModel comment)
        {
            if (ModelState.IsValid)
            {
            var newComment = new Comment() 
            {
                ArticleId = comment.ArticleId,
                AuthorId = this.User.Identity.GetUserId(),
                AuthorName = this.User.Identity.GetUserName(),
                Content = this.Sanitizer.Sanitize(comment.Content)
            };

            this.Data.Comments.Add(newComment);
            this.Data.SaveChanges();

            var newCommentModel = new CommentViewModel()
            {
                Content = this.Sanitizer.Sanitize(comment.Content),
                AuthorName = this.User.Identity.GetUserName(),
                CreatedOn = newComment.CreatedOn
            };

            return this.PartialView("_CommentPartial", newCommentModel);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, ModelState.Values.First().ToString());
        }
    }
}