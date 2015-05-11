namespace Sample2015.Web.Models.Api.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ReqDeleteAccountUser
    {
        [Required]
        public int Id { get; set; }
    }
}