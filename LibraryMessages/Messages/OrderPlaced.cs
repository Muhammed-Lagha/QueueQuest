using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMessages.Messages
{
    public sealed record class OrderPlaced(Guid OrderId, int Quantity);
}
