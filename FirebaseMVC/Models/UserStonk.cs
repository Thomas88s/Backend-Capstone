using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StonkMarket.Models
{
    public class UserStonk
    {
        public int Id { get; set; }
        public int StonkId { get; set; }
        public int UserId { get; set; }
        public bool TopPerformer { get; set; }
    }
}
