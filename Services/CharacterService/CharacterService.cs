using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnetRPG.Data;
using dotnetRPG.Dtos.Character;
using dotnetRPG.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetRPG.Services.CharacterService
{
  public class CharacterService : ICharacterService
  {
    public readonly IMapper _mapper;
    private readonly DataContext _context;

    public CharacterService(IMapper mapper, DataContext context)
    {
      _context = context;
      _mapper = mapper;

    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {
      var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

      Character character = _mapper.Map<Character>(newCharacter);
      _context.Characters.Add(character);
      await _context.SaveChangesAsync();
      serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
    {
      var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
      var dbCharacters = await _context.Characters.ToListAsync();
      serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
      var serviceResponse = new ServiceResponse<GetCharacterDto>();
      var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);

      try
      {
        serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
      }
      catch (InvalidOperationException ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }

      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
    {
      var serviceResponse = new ServiceResponse<GetCharacterDto>();

      try
      {
        Character character = await _context.Characters.FirstAsync(c => c.Id == updateCharacter.Id);

        character.Name = updateCharacter.Name;
        character.HitPoints = updateCharacter.HitPoints;
        character.Strength = updateCharacter.Strength;
        character.Defense = updateCharacter.Defense;
        character.Intelligence = updateCharacter.Intelligence;
        character.Class = updateCharacter.Class;

        await _context.SaveChangesAsync();

        serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
      }
      catch (InvalidOperationException ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }

      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
    {
      var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

      try
      {
        Character character = await _context.Characters.FirstAsync(c => c.Id == id);

        _context.Characters.Remove(character);

        await _context.SaveChangesAsync();

        serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
      }
      catch (InvalidOperationException ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }

      return serviceResponse;
    }
  }
}