using HistoryPortal.Data;
using HistoryPortal.Web.Controllers;
using HistoryPortal.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using HistoryPortal.Web.Areas.User.InputModels.Events;
using HistoryPortal.Data.Models;
using HistoryPortal.Web.Areas.User.ViewModels.Events;

namespace HistoryPortal.Web.Areas.User.Controllers
{
    [Authorize]
    public class EventsController : BaseController
    {
        private const int PageSize = 5;

        public EventsController(IHistoryPortalData data, ISanitizer sanitizer)
            : base(data, sanitizer)
        {
        }

        [HttpGet]
        public ActionResult ExpiringEvent()
        {
            var data = this.Data
                .Events
                .All()
                .Where(d => d.Date > DateTime.Now)
                .OrderBy(x => x.Date)
                .Project()
                .To<EventsViewModel>();

            return View("Details", data);
        }

        [HttpGet]
        public ActionResult Upcoming(int? id)
        {
            int pageNumber = id.GetValueOrDefault(1);

            var data = this.Data
                .Events
                .All()
                .Where(d => d.Date > DateTime.Now)
                .OrderBy(x => x.Date)
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .Project()
                .To<EventsViewModel>()
                .ToList();

            ViewBag.Pages = Math.Ceiling((double)this.Data.Events.All().Count() / PageSize);
            ViewBag.CurrPage = pageNumber;

            return View(data);
        }

        [HttpGet]
        public ActionResult Past(int? id)
        {
            int pageNumber = id.GetValueOrDefault(1);

            var data = this.Data
                .Events
                .All()
                .Where(d => d.Date <= DateTime.Now)
                .OrderByDescending(x => x.Date)
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .Project()
                .To<EventsViewModel>()
                .ToList();

            ViewBag.Pages = Math.Ceiling((double)this.Data.Events.All().Count() / PageSize);
            ViewBag.CurrPage = pageNumber;

            return View(data);
        }

        [HttpGet]
        public ActionResult Details(int Id = 1)
        {
            var article = this.Data
                .Events
                .All()
                .Where(a => a.Id == Id)
                .Project().To<EventsViewModel>()
                .FirstOrDefault();

            return View(article);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            var model = new EventsInputModel();
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventsInputModel input)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();

                var newEvent = new Event
                {
                    Title = input.Title,
                    Description = this.Sanitizer.Sanitize(input.Description),
                    Date = input.Date,
                    AuthorId = userId
                };

                this.Data.Events.Add(newEvent);
                this.Data.SaveChanges();
                return this.RedirectToAction("Upcoming");
            }

            return this.View(input);
        }
    }
}