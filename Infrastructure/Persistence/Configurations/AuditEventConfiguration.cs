using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Entities;
using Infrastructure.Common.JsonConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Infrastructure.Persistence.Configurations;

public class AuditEventConfiguration : IEntityTypeConfiguration<AuditEvent> {
    public void Configure(EntityTypeBuilder<AuditEvent> builder) {
        builder.Property(cfg => cfg.CreatedAt)
            .HasColumnName("created_at")
            .HasConversion<DateTimeOffset>();

        builder.Property(cfg => cfg.ApplicationName)
            .HasColumnName("application_name")
            .IsRequired();

        builder.Property(cfg => cfg.UpdatedBy)
            .HasColumnName("updated_by")
            .IsRequired();

        builder.Property(cfg => cfg.Location)
            .HasColumnName("location")
            .IsRequired();

        builder.Property(cfg => cfg.ApplicationScreen)
            .HasColumnName("application_screen")
            .IsRequired();

        builder.Property(cfg => cfg.ChangeType)
            .HasColumnName("change_type")
            .IsRequired();

        builder.Property(cfg => cfg.DcaId)
            .HasColumnName("dca_id")
            .IsRequired();

        builder.Property<string>(x => x.PrevState).HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions() {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    Converters = { new StringJsonConverter() },
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                }),
                v => JsonSerializer.Deserialize<string>(v, new JsonSerializerOptions() {  DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }))
            .HasColumnName("row_before")
            .HasColumnType("jsonb");

        builder.Property<string>(x => x.CurrState).HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions() {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    Converters = { new StringJsonConverter() },
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                }),
                v => JsonSerializer.Deserialize<string>(v, new JsonSerializerOptions() {  DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }))
            .HasColumnName("row_after")
            .HasColumnType("jsonb");
    }
}