namespace Domain.Common;

public abstract class BaseAuditEntity : BaseEntity<Guid> {
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}