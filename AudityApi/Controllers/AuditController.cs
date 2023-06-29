using System.Net;
using Application.Audit.Commands.CreateAudit;
using Microsoft.AspNetCore.Mvc;

namespace AudityApi.Controllers;

public class AuditController : ApiControllerBase {
    [HttpGet]
    public string Ping() {
        return "pong";
    }
    
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateAuditCommand command) {
        return await Mediator.Send(command);
    }
}