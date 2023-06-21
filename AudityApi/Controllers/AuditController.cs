using Application.Audit.Commands.CreateAudit;
using Microsoft.AspNetCore.Mvc;

namespace AudityApi.Controllers;

public class AuditController : ApiControllerBase {
    [HttpGet]
    public string Ping() {
        return "pong";
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateAuditCommand command) {
        return await Mediator.Send(command);
    }
}