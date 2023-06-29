using System.Text.Json;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Audit.Commands.CreateAudit;

public class CreateAuditCommand : IRequest<Guid> {
    public string Location { get; set; }
    public string ApplicationName { get; set; }
    public string ApplicationScreen { get; set; }
    public string DcaName { get; set; }
    public string ChangeType { get; set; }
    public JsonElement PrevState { get; set; }
    public JsonElement CurrState { get; set; }
    public Guid DcaId { get; set; }
    public Guid UpdatedBy { get; set; }
}

public class CreateAuditCommandHandler :
    IRequestHandler<CreateAuditCommand, Guid> {
    private readonly IApplicationDbContext _dbContext;

    public CreateAuditCommandHandler(IApplicationDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateAuditCommand request,
        CancellationToken cancellationToken) {
        var auditEvent = new AuditEvent {
            Location = request.Location,
            ApplicationName = request.ApplicationName,
            ApplicationScreen = request.ApplicationScreen,
            ChangeType = request.ChangeType,
            PrevState = request.PrevState,
            CurrState = request.CurrState,
            DcaId = request.DcaId,
            DcaName = request.DcaName,
            UpdatedBy = request.UpdatedBy
        };
        _dbContext.AuditEvents.Add(auditEvent);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return auditEvent.Id;
    }
}