using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetRPG.Dtos.Character;
using dotnetRPG.Models;
using dotnetRPG.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace dotnetRPG.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CharacterController : ControllerBase
  {
    public ICharacterService _characterService { get; }

    public CharacterController(ICharacterService characterService)
    {
      _characterService = characterService;

    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
    {
      return Ok(await _characterService.GetAllCharacters());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
    {
      ServiceResponse<GetCharacterDto> serviceResponse = await _characterService.GetCharacterById(id);

      if (serviceResponse.Data is null)
      {
        return NotFound(serviceResponse);
      }

      return Ok(serviceResponse);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto character)
    {
      return Ok(await _characterService.AddCharacter(character));
    }

    [HttpPut]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto updateCharacter)
    {

      ServiceResponse<GetCharacterDto> serviceResponse = await _characterService.UpdateCharacter(updateCharacter);

      if (serviceResponse.Data is null)
      {
        return NotFound(serviceResponse);
      }

      return Ok(serviceResponse);
    }

    [HttpDelete]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteCharacter(int id)
    {
      ServiceResponse<List<GetCharacterDto>> serviceResponse = await _characterService.DeleteCharacter(id);

      if (serviceResponse.Data is null)
      {
        return NotFound(serviceResponse);
      }

      return Ok(serviceResponse);
    }

  }
}