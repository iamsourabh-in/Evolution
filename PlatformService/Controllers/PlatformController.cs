using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformService.CommandDataServiceClient;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlatformService.AsyncDataServices;

namespace PlatformService.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatfomRepo _platformRepo;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public PlatformController(IPlatfomRepo platformRepo, IMapper mapper, ICommandDataClient commandDataClient,IMessageBusClient messageBusClient)
        {
            _platformRepo = platformRepo;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        // GET: PlatformController
        public ActionResult<IEnumerable<PlatformReadDto>> Index()
        {
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(_platformRepo.GetAllPlatforms()));
        }

        [HttpGet("{id}", Name = "Details")]
        // GET: PlatformController/Details/5
        public ActionResult<PlatformReadDto> Details(int id)
        {
            var platform = _mapper.Map<PlatformReadDto>(_platformRepo.GetPlatformById(id));

            if (platform == null)
                return NotFound();

            return Ok(platform);
        }


        // POST: PlatformController/Create
        [HttpPost]
        public async Task<ActionResult> Create(PlatformCreateDto platformCreateDto)
        {
            var platformCreateModel = _mapper.Map<Platform>(platformCreateDto);

            await _platformRepo.CreatePlatform(platformCreateModel);
            _platformRepo.SaveChanges();

            var readModel = _mapper.Map<PlatformReadDto>(platformCreateModel);


            try
            {
                await _commandDataClient.SendPlatformToCommand(readModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine("-> Could not send Sync");
            }

            
            try
            {
                var publishEvent = _mapper.Map<PlatformPublishedDto>(readModel);
                publishEvent.Event = "Platform Published Event";
                _messageBusClient.PublishNewPlatform(publishEvent);
            }
            catch (Exception ex)
            {
                 Console.WriteLine("-> Could not send Async");
            }

            return CreatedAtAction(nameof(Details), new { Id = platformCreateModel.Id }, readModel);

        }




    }
}
