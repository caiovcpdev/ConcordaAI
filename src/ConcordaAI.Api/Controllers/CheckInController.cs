using Asp.Versioning;
using ConcordaAI.Api.Responses;
using ConcordaAI.Application.DTOs.CheckIn;
using ConcordaAI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConcordaAI.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    public class CheckInController : ControllerBase
    {
        private readonly ICheckInService _checkInservice;

        public CheckInController(ICheckInService checkInservice)
        {
            _checkInservice = checkInservice;
        }

        [HttpPost("escalas/{escalaTrabalhadorId:guid}/checkin")]
        public async Task<IActionResult> CheckIn( Guid escalaTrabalhadorId, RegistrarCheckInRequest request)
        {
            var result = await _checkInservice
                .RegistrarCheckInAsync(escalaTrabalhadorId, request);

            if (!result.Success)
                return BadRequest(ApiResponse<string>.Fail(new[] { result.Error! }));

            return Ok(ApiResponse<string>.Ok("Check-in realizado."));
        }

        [HttpPost("escalas/{escalaTrabalhadorId:guid}/checkout")]
        public async Task<IActionResult> CheckOut(Guid escalaTrabalhadorId, RegistrarCheckOutRequest request)
        {
            var result = await _checkInservice.RegistrarCheckOutAsync(escalaTrabalhadorId, request);

            if (!result.Success)
                return BadRequest(ApiResponse<string>.Fail(new[] { result.Error! }));

            return Ok(ApiResponse<string>.Ok("Check-out realizado."));
        }

        [HttpGet("escalas/{escalaTrabalhadorId:guid}/presenca")]
        public async Task<IActionResult> Presenca(Guid escalaTrabalhadorId)
        {
            var result = await _checkInservice.ObterPresencaAsync(escalaTrabalhadorId);

            return Ok(ApiResponse<object>.Ok(result.Value));
        }
    }
}
