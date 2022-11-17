using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Messages;
using System.Runtime.InteropServices;

namespace Restaurant.Booking.DBConfigure
{
    internal class TableDB
    {
        [Table("Transaction")]
        public class Transaction
        {

            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public string messageId { get; private set; }
            [Column("OrderId", TypeName = "guid")]
            public Guid OrderId { get; private set; }
            [Column("ClientId", TypeName = "guid")]
            public Guid ClientId { get; private set; }
            [Column("PreOrder", TypeName = "guid")]
            public Dish? PreOrder { get; private set; }
            [Column("CreationDate", TypeName = "datetime")]
            public DateTime CreationDate { get; private set; }

        }
    }
}
