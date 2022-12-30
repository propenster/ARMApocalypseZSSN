using ARMApocalypseSASAPI.Dtos;
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
            string IPAddress = Request?.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            _logger.LogInformation($"IP is {IPAddress}");
            //_logger.LogInformation($"{nameof(GetAllItems)} Get Customer Limit Request  - {JsonConvert.SerializeObject(request)} AT TIMESTAMPS: {DateTime.Now}");

            var result = await _coreService.GetAllItems();

            _logger.LogInformation($"{nameof(GetAllItems)} GetAllItems Request  - RESPONSE: {JsonConvert.SerializeObject(result)} AT TIMESTAMPS: {DateTime.Now}");

            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost, Route("survivors/register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<SurvivorResponse>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType, Type = typeof(DefaultErrorResponse))]
        [ProducesDefaultResponseType(typeof(DefaultErrorResponse))]
        public async Task<IActionResult> RegisterSurvivor([FromBody] RegisterSurvivorRequest request)
        {
            string IPAddress = Request?.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            _logger.LogInformation($"IP is {IPAddress}");
            _logger.LogInformation($"{nameof(RegisterSurvivor)} RegisterSurvivor Request  - {JsonConvert.SerializeObject(request)} AT TIMESTAMPS: {DateTime.Now}");

            var result = await _coreService.RegisterSurvivor(request) ;

            _logger.LogInformation($"{nameof(RegisterSurvivor)} RegisterSurvivor Request  - RESPONSE: {JsonConvert.SerializeObject(result)} AT TIMESTAMPS: {DateTime.Now}");

            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut, Route("survivors/location/update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<SurvivorResponse>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType, Type = typeof(DefaultErrorResponse))]
        [ProducesDefaultResponseType(typeof(DefaultErrorResponse))]
        public async Task<IActionResult> UpdateSurvivorLocation([FromBody] UpdateSurvivorLocationRequest request)
        {
            string IPAddress = Request?.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            _logger.LogInformation($"IP is {IPAddress}");
            _logger.LogInformation($"{nameof(UpdateSurvivorLocation)} UpdateSurvivorLocation Request  - {JsonConvert.SerializeObject(request)} AT TIMESTAMPS: {DateTime.Now}");

            var result = await _coreService.UpdateSurvivorLocation(request);

            _logger.LogInformation($"{nameof(UpdateSurvivorLocation)} UpdateSurvivorLocation Request  - RESPONSE: {JsonConvert.SerializeObject(result)} AT TIMESTAMPS: {DateTime.Now}");

            return StatusCode((int)result.StatusCode, result);
        }
        [HttpPost, Route("survivors/infectionstatus/update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<SurvivorInfectionReportResponse>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType, Type = typeof(DefaultErrorResponse))]
        [ProducesDefaultResponseType(typeof(DefaultErrorResponse))]
        public async Task<IActionResult> ReportSurvivorInfectionStatus([FromBody] UpdateSurvivorInfectionStatusRequest request)
        {
            string IPAddress = Request?.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            _logger.LogInformation($"IP is {IPAddress}");
            _logger.LogInformation($"{nameof(ReportSurvivorInfectionStatus)} ReportInfectionStatus Request  - {JsonConvert.SerializeObject(request)} AT TIMESTAMPS: {DateTime.Now}");

            var result = await _coreService.ReportSurvivorInfectionStatus(request);

            _logger.LogInformation($"{nameof(ReportSurvivorInfectionStatus)} ReportInfectionStatus Request  - RESPONSE: {JsonConvert.SerializeObject(result)} AT TIMESTAMPS: {DateTime.Now}");

            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut, Route("survivors/infectionstatus/flag")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<SurvivorResponse>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType, Type = typeof(DefaultErrorResponse))]
        [ProducesDefaultResponseType(typeof(DefaultErrorResponse))]
        public async Task<IActionResult> FlagSurvivorAsInfected([FromBody] FlagSurvivorInfectedRequest request)
        {
            string IPAddress = Request?.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            _logger.LogInformation($"IP is {IPAddress}");
            _logger.LogInformation($"{nameof(ReportSurvivorInfectionStatus)} FlagSurvivorInfected Request  - {JsonConvert.SerializeObject(request)} AT TIMESTAMPS: {DateTime.Now}");

            var result = await _coreService.FlagSurvivorAsInfected(request);

            _logger.LogInformation($"{nameof(ReportSurvivorInfectionStatus)} FlagSurvivorInfected Request  - RESPONSE: {JsonConvert.SerializeObject(result)} AT TIMESTAMPS: {DateTime.Now}");

            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut, Route("trade/create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<object>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType, Type = typeof(DefaultErrorResponse))]
        [ProducesDefaultResponseType(typeof(DefaultErrorResponse))]
        public async Task<IActionResult> CreateTrade([FromBody] TradingRequest request)
        {
            string IPAddress = Request?.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            _logger.LogInformation($"IP is {IPAddress}");
            _logger.LogInformation($"{nameof(CreateTrade)} CreateTrade Request  - {JsonConvert.SerializeObject(request)} AT TIMESTAMPS: {DateTime.Now}");

            var result = await _coreService.Trade(request);

            _logger.LogInformation($"{nameof(CreateTrade)} CreateTrade Request  - RESPONSE: {JsonConvert.SerializeObject(result)} AT TIMESTAMPS: {DateTime.Now}");

            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet, Route("reports/all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<ReportResponse>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(DefaultErrorResponse))]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType, Type = typeof(DefaultErrorResponse))]
        [ProducesDefaultResponseType(typeof(DefaultErrorResponse))]
        public async Task<IActionResult> FetchReport()
        {
            string IPAddress = Request?.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            _logger.LogInformation($"IP is {IPAddress}");
            //_logger.LogInformation($"{nameof(GetReport)} GetReport Request  - {JsonConvert.SerializeObject(request)} AT TIMESTAMPS: {DateTime.Now}");

            var result = await _coreService.FetchReport();

            _logger.LogInformation($"{nameof(FetchReport)} FetchReport Request  - RESPONSE: {JsonConvert.SerializeObject(result)} AT TIMESTAMPS: {DateTime.Now}");

            return StatusCode((int)result.StatusCode, result);
        }
    }
}
