namespace Sample2015.Web.Models.Api.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ReqCreateAccountUser
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PasswordCheck { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }
    }
}