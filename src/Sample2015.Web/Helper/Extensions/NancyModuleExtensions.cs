namespace Sample2015.Web.Helper.Extensions
{
    using Nancy;
    using Nancy.Responses.Negotiation;

    public static class NancyNegotiateExtensions
    {
        public static readonly string JsonContentType = "application/json";

        public static Negotiator WithOnlyJson<T>(this Negotiator nego, T model, HttpStatusCode code = HttpStatusCode.OK)
        {
            return nego.WithStatusCode(code)
                .WithAllowedMediaRange(new MediaRange(JsonContentType))
                .WithMediaRangeModel(JsonContentType, model);
        }
    }
}