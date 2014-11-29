using AutoMapper;
using HistoryPortal.Data.Models;
using HistoryPortal.Web.Areas.User.Views.EventComments;
using HistoryPortal.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HistoryPortal.Web.Areas.User.ViewModels.Events
{
    public class EventsViewModel : IMapFrom<Event>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public DateTime Date { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Ups { get; set; }

        public int Downs { get; set; }

        public IEnumerable<EventCommentsViewModel> Comments { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Event, EventsViewModel>()
               .ForMember(a => a.Author, opt => opt.MapFrom(u => u.Author.UserName))
               .ForMember(a => a.Ups, opt => opt.MapFrom(u => u.Votes.Where(v => v.State == VoteState.Up).Count()))
               .ForMember(a => a.Downs, opt => opt.MapFrom(u => u.Votes.Where(v => v.State == VoteState.Down).Count()))
               .ReverseMap();
        }
    }
}