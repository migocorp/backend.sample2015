namespace Sample2015.Web.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Responses.Negotiation;
    using Nancy.Validation;

    [Serializable]
    public class HttpException : Exception
    {
        public HttpException(HttpStatusCode statusCode, object content)
        {
            this.StatusCode = statusCode;
            this.Content = content;
        }

        public HttpException(HttpStatusCode statusCode)
            : this(statusCode, string.Empty)
        {
        }

        public HttpException()
            : this(HttpStatusCode.InternalServerError, string.Empty)
        {
        }

        public HttpStatusCode StatusCode { get; private set; }

        public object Content { get; private set; }

        public static HttpException NotFound(object content)
        {
            return new HttpException(HttpStatusCode.NotFound, content);
        }

        public static Exception InternalServerError(object content)
        {
            return new HttpException(HttpStatusCode.InternalServerError, content);
        }
    }
}