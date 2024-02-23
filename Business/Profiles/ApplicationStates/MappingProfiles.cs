using AutoMapper;
using Business.Requests.ApplicationStates;
using Business.Responses.ApplicationStates;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles.ApplicationStates
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<ApplicationState, CreateApplicationStateRequest>().ReverseMap();
            CreateMap<ApplicationState, DeleteApplicationStateRequest>().ReverseMap();
            CreateMap<ApplicationState, UpdateApplicationStateRequest>().ReverseMap();

            CreateMap<ApplicationState, GetAllApplicationStateResponse>().ReverseMap();
            CreateMap<ApplicationState, CreateApplicationStateResponse>().ReverseMap();
            CreateMap<ApplicationState, DeleteApplicationStateResponse>().ReverseMap();
            CreateMap<ApplicationState, GetByIdApplicationStateResponse>().ReverseMap();
            CreateMap<ApplicationState, UpdateApplicationStateResponse>().ReverseMap();

        }
    }
}
