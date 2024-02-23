using AutoMapper;
using Business.Requests.Bootcamps;
using Business.Responses.Bootcamps;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles.Bootcamps
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<Bootcamp, CreateBootcampRequest>().ReverseMap();
            CreateMap<Bootcamp, DeleteBootcampRequest>().ReverseMap();
            CreateMap<Bootcamp, UpdateBootcampRequest>().ReverseMap();

            CreateMap<Bootcamp, GetAllBootcampResponse>().ReverseMap();
            CreateMap<Bootcamp, CreateBootcampResponse>().ReverseMap();
            CreateMap<Bootcamp, DeleteBootcampResponse>().ReverseMap();
            CreateMap<Bootcamp, GetByIdBootcampResponse>().ReverseMap();
            CreateMap<Bootcamp, UpdateBootcampResponse>().ReverseMap();
        }
    }
}
