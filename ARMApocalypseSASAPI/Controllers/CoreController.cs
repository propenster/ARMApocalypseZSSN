﻿using ARMApocalypseSASAPI.Dtos;
using ARMApocalypseSASAPI.Helpers;
using ARMApocalypseSASAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace ARMApocalypseSASAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/core")]
    public class CoreController : ControllerBase
    {
        private readonly ICoreService _coreService;
        private readonly ILogger _logger;

        public CoreController(ICoreService coreService, ILogger<CoreController> logger)
        {
            _coreService = coreService;
            _logger = logger;
        }

        [HttpGet, Route("items/all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<ItemResponse>>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType, Type = typeof(DefaultErrorResponse))]
        [ProducesDefaultResponseType(typeof(DefaultErrorResponse))]
        public async Task<IActionResult> GetAllItems()
        {
            string IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            _logger.LogInformation($"IP is {IPAddress}");
            //_logger.LogInformation($"{nameof(GetAllItems)} Get Customer Limit Request  - {JsonConvert.SerializeObject(request)} AT TIMESTAMPS: {DateTime.Now}");

            var result = await _coreService.GetAllItems();

            _logger.LogInformation($"{nameof(GetAllItems)} GetAllItems Request  - RESPONSE: {JsonConvert.SerializeObject(result)} AT TIMESTAMPS: {DateTime.Now}");

            return StatusCode((int)HttpStatusCode.OK, result);


        }





    }
}