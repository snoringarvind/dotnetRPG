
using System.Security.Claims;
using AutoMapper;
using dotnetRPG.Data;
using dotnetRPG.Dtos.Character;
using dotnetRPG.Dtos.Weapon;
using dotnetRPG.Models;
using dotnetRPG.Services.CharacterService;
using Microsoft.EntityFrameworkCore;

namespace dotnetRPG.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ICharacterService _characterService;

        public WeaponService(IHttpContextAccessor httpContextAccessor, DataContext context, IMapper mapper, ICharacterService characterService)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
            _characterService = characterService;
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();

            try
            {
                //ask on reddit
                Character character = await _context.Characters.FirstAsync(c => c.Id == newWeapon.CharacterId && c.User.Id == _characterService.GetUserId());

                Weapon weapon = new Weapon()
                {
                    Damage = newWeapon.Damage,
                    Character = character,
                    Name = newWeapon.Name
                };

                _context.Weapons.Add(weapon);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetCharacterDto>(character);

            }
            catch (InvalidOperationException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            catch (DbUpdateException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}