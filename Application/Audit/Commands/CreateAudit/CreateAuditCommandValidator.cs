using FluentValidation;

namespace Application.Audit.Commands.CreateAudit;

public class CreateAuditCommandValidator : AbstractValidator<CreateAuditCommand> {
    public CreateAuditCommandValidator() {
        RuleFor(v => v.UpdatedBy)
            .NotEmpty();

        RuleFor(v => v.DcaId)
            .NotEmpty();
        
        RuleFor(v => v.PrevState)
            .NotEmpty();
        
        RuleFor(v => v.CurrState)
            .NotEmpty();
        
        RuleFor(v => v.Location)
            .NotEmpty();
        
        RuleFor(v => v.ApplicationName)
            .NotEmpty();
        
        RuleFor(v => v.ApplicationScreen)
            .NotEmpty();
        
        RuleFor(v => v.ChangeType)
            .NotEmpty();
    }
}