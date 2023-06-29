using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Domain.Common;

namespace Domain.Entities;

[Table("audit")]
public class AuditEvent : BaseAuditEntity {
    public string? Location { get; set; }
    public string? ApplicationName { get; set; }
    public string? ApplicationScreen { get; set; }
    public string? ChangeType { get; set; }
    [Column(TypeName = "jsonb")] public JsonElement PrevState { get; set; }
    [Column(TypeName = "jsonb")] public JsonElement CurrState { get; set; }
    public Guid DcaId { get; set; }
    public string DcaName { get; set; }
    public Guid UpdatedBy { get; init; }
}