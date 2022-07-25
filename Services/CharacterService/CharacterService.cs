using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly IHttpContextAccessor _contextAccessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
            _mapper = mapper;

        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
                Character character = _mapper.Map<Character>(newCharacter);
                character.User = await _context.Users.FirstAsync(u => u.Id == GetUserId());
                _context.Characters.Add(character);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Characters
                      .Include(c=>c.Skills)
                      .Include(c=>c.Weapon)
                      .Where(c => c.User.Id == GetUserId())
                      .Select(c => _mapper.Map<GetCharacterDto>(c))
                      .ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            serviceResponse.Data = await _context.Characters
                    .Include(c=>c.Skills)
                    .Include(c=>c.Weapon)
                    .Where(c => c.User.Id == GetUserId())
                    .Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                var dbCharacter = await _context.Characters
                      .Include(c=>c.Skills)
                      .Include(c=>c.Weapon)
                      .FirstAsync(c => c.Id == id && c.User.Id == GetUserId());

                Console.WriteLine("dbCharacter == " + dbCharacter);
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
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                Character character = await _context.Characters
                .Include(c => c.User)
                .Include(c=>c.Skills)
                .Include(c=>c.Weapon)
                .FirstAsync(c => c.Id == updateCharacter.Id);

                if (character.User.Id == GetUserId())
                {

                    character.Name = updateCharacter.Name;
                    character.HitPoints = updateCharacter.HitPoints;
                    character.Strength = updateCharacter.Strength;
                    character.Defense = updateCharacter.Defense;
                    character.Intelligence = updateCharacter.Intelligence;
                    character.Class = updateCharacter.Class;

                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found.";
                }

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
                Character character = await _context.Characters
                    .Include(c=>c.Skills)
                    .Include(c=>c.Weapon)
                    .FirstAsync(c => c.Id == id && c.User.Id == GetUserId());

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

        private int GetUserId() =>
            int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            var response = new ServiceResponse<GetCharacterDto>();

            try
            {
                var character = await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId && c.User.Id == GetUserId());

                if (character == null)
                {
                    response.Success = false;
                    response.Message = "Character Not Found.";
                    return response;
                }

                var skill = await _context.Skills
                    .FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);

                if (skill == null)
                {
                    response.Success = false;
                    response.Message = "Skill Not Found.";
                    return response;
                }

                character.Skills.Add(skill);

                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDto>(character);

            }
            catch (Exception ex)
            {
                Console.WriteLine("here's the problem " + ex);
                response.Success = false;
                response.Message = ex.Message;

            }
            return response;

        }
    }
}