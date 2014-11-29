using HistoryPortal.Data;
using HistoryPortal.Data.Models;
using HistoryPortal.Web.Controllers;
using HistoryPortal.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HistoryPortal.Web.ViewModels.Votes;
using HistoryPortal.Web.Areas.User.ViewModels.Events;

namespace HistoryPortal.Web.Areas.User.Controllers
{
    [Authorize]
    public class VotesController : BaseController
    {
        public VotesController(IHistoryPortalData data, ISanitizer sanitizer)
            : base(data, sanitizer)
        {
        }

        [HttpPost]
        [Authorize]
        public ActionResult Up(int Id)
        {
            var model = this.Vote(Id, VoteState.Up);
            if (model == null)
            {
                return this.PartialView();
            }

            return this.PartialView("_VotesPartial", model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Down(int Id)
        {
            var model = this.Vote(Id, VoteState.Down);
            if (model == null)
            {
                return this.PartialView();
            }

            return this.PartialView("_VotesPartial", model);
        }

        private EventsViewModel Vote(int Id, VoteState newVote)
        {
            var currEvent = this.Data
                .Events
                .All()
                .FirstOrDefault(a => a.Id == Id);

            var vote = this.GetUserVote(currEvent);

            if (vote != null && vote.State == newVote)
            {
                return this.Data
                   .Events
                   .All()
                   .Where(a => a.Id == Id)
                   .Project().To<EventsViewModel>()
                   .FirstOrDefault();
            }

            if (vote != null && vote.State != newVote)
            {
                currEvent.Votes.Remove(vote);
            }

            var voteModel = this.AddVote(currEvent, newVote);
            return voteModel;
        }

        private Vote GetUserVote(Event currEvent)
        {
            var vote = currEvent
                .Votes
                .FirstOrDefault(x => x.UserId == this.User.Identity.GetUserId());

            return vote;
        }

        private EventsViewModel AddVote(Event currEvent, VoteState state)
        {
            if (currEvent == null)
            {
                ModelState.AddModelError("ArticleId", "Article not founed!");
            }

            var uId = this.User.Identity.GetUserId();

            var vote = new Vote()
            {
                State = state,
                UserId = this.User.Identity.GetUserId()
            };

            currEvent.Votes.Add(vote);
            this.Data.SaveChanges();

            var upsCount = currEvent.Votes.Where(v => v.State == VoteState.Up).Count();
            var downsCount = currEvent.Votes.Where(v => v.State == VoteState.Down).Count();

            var newVoteModel = new VotesViewModel()
            {
                Ups = upsCount,
                Downs = downsCount
            };

            var eventt = this.Data
                .Events
                .All()
                .Where(a => a.Id == currEvent.Id)
                .Project().To<EventsViewModel>()
                .FirstOrDefault();

            return eventt;
        }
    }
}