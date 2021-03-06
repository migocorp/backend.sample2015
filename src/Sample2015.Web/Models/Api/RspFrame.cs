﻿// <auto-generated/>

namespace Sample2015.Web.Models.Api
{
    using Nancy;

    public class RspFrame
    {
        public RspFrame()
        {
            this.code = 0;
            this.msg = string.Empty;
        }

        public RspFrame(HttpStatusCode httpCode, string msg = "")
        {
            this.code = (int)httpCode;
            if (string.IsNullOrEmpty(msg))
            {
                this.msg = httpCode.ToString();
            }
            else
            {
                this.msg = msg;
            }
        }

        public int code { get; set; }

        public string msg { get; set; }
    }
}