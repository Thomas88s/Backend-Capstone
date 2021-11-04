using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StonkMarket.Models.ViewModels
{
    public class MessageViewModel
    {
        public Message Message { get; set; }
        public List<UserProfile> UserProfiles { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}
