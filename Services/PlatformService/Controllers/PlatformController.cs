using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformService.SyncDataServices;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlatformService.AsyncDataServices;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using PlatformService.Policies;

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
        private readonly IConfiguration _config;
        private readonly ClientPolicy _clientPolicy;
        private readonly IHttpClientFactory _httpClientFactory;

        public PlatformController(
            IHttpClientFactory httpClientFactory,
            ClientPolicy clientPolicy,
            IPlatfomRepo platformRepo,
            IMapper mapper,
            ICommandDataClient commandDataClient,
            IMessageBusClient messageBusClient,
            IConfiguration config
        )
        {
            _platformRepo = platformRepo;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
            _config = config;
            _clientPolicy = clientPolicy;
            _httpClientFactory = httpClientFactory;
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
                publishEvent.Event = "Platform_Published";
                _messageBusClient.PublishNewPlatform(publishEvent);
            }
            catch (Exception ex)
            {
                Console.WriteLine("-> Could not send Async");
            }

            return CreatedAtAction(nameof(Details), new { Id = platformCreateModel.Id }, readModel);
        }

        // POST: PlatformController/Create
        [HttpGet("Robust")]
        public async Task<ActionResult> Robust(PlatformCreateDto platformCreateDto)
        {
            Console.WriteLine("Entered Robust");

            var client = _httpClientFactory.CreateClient(); 

            //var response = await client.GetAsync(_config["CommandService"]);

            var response = await _clientPolicy.ExceptionPolicy.ExecuteAsync(
                () => {Console.WriteLine("Entered Policy");var response = client.GetAsync(_config["CommandService"]); return response;}
            );

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Response Service");
                return Ok();
            }

            return StatusCode(StatusCodes.Status500InternalServerError);

            // var platformCreateModel = _mapper.Map<Platform>(platformCreateDto);

            // await _platformRepo.CreatePlatform(platformCreateModel);
            // _platformRepo.SaveChanges();

            // var readModel = _mapper.Map<PlatformReadDto>(platformCreateModel);


            // try
            // {
            //     await _commandDataClient.SendPlatformToCommand(readModel);
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine("-> Could not send Sync");
            // }


            // return CreatedAtAction(nameof(Details), new { Id = platformCreateModel.Id }, response);

        }
    }
}
