using System.Reflection;
using System.Text.Json;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext {
    private readonly IMediator _mediator;

    public DbSet<AuditEvent> AuditEvents => Set<AuditEvent>();
    
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IMediator mediator
    ) : base(options) {
        _mediator = mediator;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}