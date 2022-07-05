using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetRPG.Dtos.Character;
using dotnetRPG.Models;

namespace dotnetRPG.Services.CharacterService
{
  public interface ICharacterService
  {
    Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
    Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
    Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);
    Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter);
    Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);

  }
}