using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xend.API.Commands;

namespace Xend.API.Controllers
{
    [Route("api/1/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly IMediator _mediator;
       

        public TransactionController(ILogger<TransactionController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool),200)]
        public async Task<IActionResult> RecievePayment(TransactionServiceCommand command)
        {
            try
            {
                var resp = await _mediator.Send(command);
                return Ok(resp);

                
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message, e);
                throw;
            }
        }
    }
}
