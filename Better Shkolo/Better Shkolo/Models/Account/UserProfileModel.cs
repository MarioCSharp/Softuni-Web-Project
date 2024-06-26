﻿namespace BetterShkolo.Models.Account
{
    public class UserProfileModel
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string DoctorName { get; set; } = null!;
        public string DoctorPhone { get; set; } = null!;
        public string DoctorAddress { get; set; } = null!;
        public DateTime BirthDate { get; set; }
    }
}
