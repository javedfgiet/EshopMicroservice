using Microsoft.EntityFrameworkCore.Diagnostics;
using MediatR;
namespace Ordering.Infrastructure.Data.Interceptors
{
    public class DispatchDomainEventsInterceptor(IMediator mediatr):SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChanges(eventData, result);
        }
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await DispatchDomainEvents(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public async Task DispatchDomainEvents(DbContext? context)
        {
            if (context == null) return;

            var aggregate=context.ChangeTracker
                                 .Entries<IAggregate>()
                                 .Where(a => a.Entity.DomainEvents.Any())
                                 .Select(a => a.Entity);



            var domainEvents = aggregate.SelectMany(a => a.DomainEvents).ToList();

            aggregate.ToList().ForEach(a=>a.ClearDomainEvents());

            foreach (var domainEvent in domainEvents) 
            { 

                await mediatr.Publish(domainEvent);
            
            }
        }
    }
}
