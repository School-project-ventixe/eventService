using eventService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace eventService.Data.Context
{
    public class DataContexts(DbContextOptions<DataContexts> options) : DbContext(options)
    {
        public DbSet<EventEntity> Events { get; set; }
    }
}
