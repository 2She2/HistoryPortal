using HistoryPortal.Data;
using HistoryPortal.Data.Models;
using HistoryPortal.Web.Areas.User.InputModels.Comments;
using HistoryPortal.Web.Controllers;
using HistoryPortal.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using HistoryPortal.Web.ViewModels.Comments;

namespace HistoryPortal.Web.Areas.User.Controllers
{
    public class CommentsController : BaseController
    {
        public CommentsController(IHistoryPortalData data, ISanitizer sanitizer)
            : base(data, sanitizer)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult PostComment(CommentInputModel comment)
        {
            if (ModelState.IsValid)
            {
                var newComment = new EventComment()
                {
                    EventId = comment.EventId,
                    AuthorId = this.User.Identity.GetUserId(),
                    AuthorName = this.User.Identity.GetUserName(),
                    Content = this.Sanitizer.Sanitize(comment.Content)
                };

                this.Data.EventComments.Add(newComment);
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