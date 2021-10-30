using System;
using System.Collections.Generic;


namespace StonkMarket.Models.ViewModels
{
    public class UserStonksViewModel
    {
        public UserProfile UserProfile { get; set; }
        public List<UserStonk> UserStonk { get; set; }
        public List<Stonk> Stonks { get; set; }
        public List<Message> message { get; set; }

    }
}
