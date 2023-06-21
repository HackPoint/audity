using Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common; 


public static class MediatorExtensions {
    public static async Task DispatchDomainEvents<TId>(this IMediator mediator, DbContext context) {
        /*var entities = context.ChangeTracker
            .Entries<BaseEntity<TId>>()
            .Where(e => e.EntityAny())
            .Select(e => e.Entity);

        var domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        entities.ToList().ForEach(e => e.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);*/
    }
}