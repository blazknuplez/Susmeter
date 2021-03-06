﻿using AutoMapper;
using Susmeter.Abstractions.Models;
using Susmeter.Ef.Entities;

namespace Susmeter.DataAccess.Infrastructure
{
    public class DbMappingProfile : Profile
    {
        public DbMappingProfile()
        {
            CreateMap<PlayerEntity, Player>()
                .ForMember(d => d.AvatarColor, f => f.Ignore());

            CreateMap<MatchEntity, Match>()
                .ForMember(d => d.Players, f => f.MapFrom(s => s.Players));

            CreateMap<MatchPlayerEntity, MatchPlayer>()
                .ForMember(d => d.PlayerId, f => f.MapFrom(s => s.PlayerId))
                .ForMember(d => d.Name, f => f.MapFrom(s => s.Player.Name))
                .ForMember(d => d.Nickname, f => f.MapFrom(s => s.Player.Nickname))
                .ForMember(d => d.AvatarHexColor, f => f.MapFrom(s => s.Player.AvatarHexColor))
                .ForMember(d => d.Color, f => f.Ignore());
        }
    }
}
