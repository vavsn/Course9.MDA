using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Booking.DBConfigure
{
    internal class TransactionDBServiceContext : DbContext
    {

        public virtual DbSet<TableDB> Clients { get; set; }
        public TransactionDBServiceContext(DbContextOptions options) : base(options) { }

    }
}
