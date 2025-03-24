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
        
        [HttpGet]
        public async Task<ActionResult<HobbyResponse>> GetHobbyByName([FromQuery] string name, CancellationToken cancellationToken)
        {
            var hobby = await _hobbyService.GetHobbyByNameAsync(name, cancellationToken);
            if (hobby is null){
                return NotFound();
            }
            return Ok(hobby.ToDto());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHobbyById(int id, CancellationToken cancellationToken)
        {
            var deleted = await _hobbyService.DeleteHobbyByIdAsync(id, cancellationToken);
            if(deleted){
                return NoContent(); //204
            }
            return NotFound(); //404
        }
    }
