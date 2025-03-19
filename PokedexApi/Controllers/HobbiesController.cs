using Microsoft.AspNetCore.Mvc;
using PokedexApi.Services;
using PokedexApi.Dtos;
using PokedexApi.Mappers;

namespace PokedexApi.Controllers.HobbiesController;

    [ApiController]
    [Route("api/v1/[controller]")]
    public class HobbiesController : ControllerBase
    {   
        private readonly IHobbyService _hobbyService;
        public HobbiesController(IHobbyService hobbyService)
        {
            _hobbyService = hobbyService;
        }

        //localhost/api/v1/hobbies/123433-84848
        [HttpGet("{id}")]
        public async Task<ActionResult<HobbyResponse>> GetHobbyById(int id, CancellationToken cancellationToken)
        {
            var hobby = await _hobbyService.GetHobbyByIdAsync(id, cancellationToken);
            if (hobby is null){
                return NotFound();
            }
            return Ok(hobby.ToDto());
        }   
        
        [HttpGet("Nombre/{name}")]
        public async Task<ActionResult<HobbyResponse>> GetHobbyByName(string name, CancellationToken cancellationToken)
        {
            var hobby = await _hobbyService.GetHobbyByNameAsync(name, cancellationToken);
            if (hobby is null){
                return NotFound();
            }
            return Ok(hobby.ToDto());
        }
    }
