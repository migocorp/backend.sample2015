﻿namespace Sample2015.Web.Models.Api.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ReqUpdateAccountUser
    {
        [Required]
        public int Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }
    }
}