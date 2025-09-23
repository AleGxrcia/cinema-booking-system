﻿using CinemaBookingSystem.Shared.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CinemaBookingSystem.Shared.Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void UpdateEntities(DbContext? context)
    {
        if (context is null) return;

        var currentTime = DateTime.UtcNow;

        foreach (var entry in context.ChangeTracker.Entries<Entity<Guid>>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    // TODO: CreatedBy
                    break;
                case EntityState.Modified:
                    entry.Entity.MarkAsUpdated();
                    break;
            }
        }
    }
}
