using AutoMapper;
using HistoryPortal.Data.Models;
using HistoryPortal.Web.Areas.Administration.ViewModels.Base;
using HistoryPortal.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HistoryPortal.Web.Areas.Administration.ViewModels.EventsAdmin
{
    public class EventsAdminViewModel : AdministrationViewModel, IMapFrom<Event>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Title { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string Author { get; set; }

        public int Comments { get; set; }

        public int Votes { get; set; }

        public int Users { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Event, EventsAdminViewModel>()
               .ForMember(a => a.Author, v => v.MapFrom(a => a.Author.UserName))
               .ForMember(a => a.Comments, v => v.MapFrom(a => a.Comments.Count))
               .ForMember(a => a.Votes, v => v.MapFrom(a => a.Votes.Count))
               .ForMember(a => a.Users, v => v.MapFrom(a => a.Users.Count))
               .ReverseMap();
        }
    }
}