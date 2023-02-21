using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly ICommandDataClient _cmdDtaClient;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public PlatformsController(AppDbContext context, IMapper mapper, ICommandDataClient cmdDtaClient)
        {
            _cmdDtaClient = cmdDtaClient;
            _appDbContext = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var entities = _appDbContext.Platorms.ToList();
            var res = _mapper.Map<List<PlatformReadDto>>(entities);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlatformCreateDto model)
        {
            if (ModelState.IsValid)
            {
                var paltform = _mapper.Map<Platform>(model);
                _appDbContext.Platorms.Add(paltform);
                _appDbContext.SaveChanges();

                try
                {
                    var readDto = _mapper.Map<PlatformReadDto>(paltform);
                    await _cmdDtaClient.SendPlatformToCommand(readDto);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.ToString());
                }

                return Ok();
            }

            return BadRequest();
        }
    }
}
