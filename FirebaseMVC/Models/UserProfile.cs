using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace StonkMarket.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string FirebaseUserId { get; set; }
        [DisplayName("User")]
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public  DateTime? CreatedDate { get; set; }
        public string Email { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
