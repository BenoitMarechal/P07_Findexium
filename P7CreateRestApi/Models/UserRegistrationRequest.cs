﻿namespace P7CreateRestApi.Models
{
    public class UserRegistrationRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }

}
