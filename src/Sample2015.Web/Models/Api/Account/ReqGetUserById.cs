namespace Sample2015.Web.Models.Api.Account
{
    using System.ComponentModel.DataAnnotations;

    public class ReqGetUserById
    {
        [Required]
        public int Id { get; set; }
    }
}