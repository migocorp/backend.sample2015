[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]

namespace Sample2015.Web.Models.Api.Account
{
    using System;
    using System.Collections.Generic;

    public class ReqGetUserByName
    {
        public string username { get; set; }
    }
}