using AutoMapper;
using Business.Requests.Blacklists;
using Business.Responses.Blacklists;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles.Blacklists
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<Blacklist, CreateBlacklistRequest>().ReverseMap();
            CreateMap<Blacklist, DeleteBlacklistRequest>().ReverseMap();
            CreateMap<Blacklist, UpdateBlacklistRequest>().ReverseMap();

            CreateMap<Blacklist, GetAllBlacklistResponse>().ReverseMap();
            CreateMap<Blacklist, CreateBlacklistResponse>().ReverseMap();
            CreateMap<Blacklist, DeleteBlacklistResponse>().ReverseMap();
            CreateMap<Blacklist, GetByIdBlacklistResponse>().ReverseMap();
            CreateMap<Blacklist, UpdateBlacklistResponse>().ReverseMap();
            CreateMap<Blacklist, GetByApplicantIdResponse>().ReverseMap();
        }
    }
}
