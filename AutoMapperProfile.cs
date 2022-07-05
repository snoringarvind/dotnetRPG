using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnetRPG.Dtos.Character;
using dotnetRPG.Models;

namespace dotnetRPG
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      CreateMap<Character, GetCharacterDto>();
      CreateMap<AddCharacterDto, Character>();
    }
  }
}