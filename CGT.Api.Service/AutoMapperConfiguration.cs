using AutoMapper;
using CGT.Entity.CgtTravelModel;
using CGT.Event.Model.Manage;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.Service
{
    public static class AutoMapperConfiguration
    {

        public static void RegisterMappings() {

            Mapper.Initialize(cgt => {
                cgt.CreateMap<BaseRiskModel,TravelBatchOrder>()
                 .ForMember(dest=>dest.PassengerName,opt=>opt.MapFrom(src=>src.PersonName))
                 .ForMember(dest => dest.CreateTime, opt=>opt.MapFrom(src =>DateTime.Now))
                 .ForMember(dest => dest.TicketNo, opt => opt.MapFrom(src => src.TicketNum.Replace("-","")));
            });
        }
    }
}
