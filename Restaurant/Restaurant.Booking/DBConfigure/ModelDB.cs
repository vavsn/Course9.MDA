using Restaurant.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Booking.DBConfigure
{
    public class ModelDB
    {
        public string messageId { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
        public Dish? PreOrder { get; private set; }
        public DateTime CreationDate { get; private set; }
    }

}
