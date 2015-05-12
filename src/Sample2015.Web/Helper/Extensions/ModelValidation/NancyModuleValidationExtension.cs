namespace Sample2015.Web.Helper.Extensions.ModelValidation
{
    using System;
    using System.Threading.Tasks;
    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Responses.Negotiation;
    using Nancy.Validation;
    using Sample2015.Web.Helper;
    using Sample2015.Web.Models.Api;

    public static class NancyModuleValidationExtension
    {
        public static void GetHandler<TOut>(this NancyModule module, string path, Func<TOut> handler)
        {
            module.Get[path] = _ => RunHandler(module, handler);
        }

        public static void GetHandler<TIn, TOut>(this NancyModule module, string path, Func<TIn, TOut> handler)
        {
            module.Get[path] = _ => RunHandler(module, handler);
        }

        public static void GetHandlerAsync<TIn, TOut>(this NancyModule module, string path, Func<TIn, Task<TOut>> handler)
        {
            module.Get[path, true] = async (x, ctx) => await RunHandlerAsync(module, handler);
        }

        public static void GetHandlerAsync<TOut>(this NancyModule module, string path, Func<Task<TOut>> handler)
        {
            module.Get[path, true] = async (x, ctx) => await RunHandlerAsync(module, handler);
        }

        public static object RunHandler<TOut>(this NancyModule module, Func<TOut> handler)
        {
            try
            {
                return handler();
            }
            catch (HttpException hEx)
            {
                return module.Negotiate.WithStatusCode(hEx.StatusCode).WithModel(hEx.Content);
            }
        }

        public static async Task<object> RunHandlerAsync<TOut>(this NancyModule module, Func<Task<TOut>> handler)
        {
            try
            {
                TOut result = await handler();
                return result;
            }
            catch (HttpException hEx)
            {
                return module.Negotiate.WithStatusCode(hEx.StatusCode).WithModel(hEx.Content);
            }
        }

        public static object RunHandler<TIn, TOut>(this NancyModule module, Func<TIn, TOut> handler)
        {
            try
            {
                TIn model;
                try
                {
                    model = module.BindAndValidate<TIn>();
                    if (!module.ModelValidationResult.IsValid)
                    {
                        return module.Negotiate.RespondWithValidationFailure(module.ModelValidationResult);
                    }
                }
                catch (ModelBindingException ex)
                {
                    var propNames = string.Empty;
                    var propsEx = ex.PropertyBindingExceptions;
                    if (propsEx != null)
                    {
                        foreach (PropertyBindingException exp in propsEx)
                        {
                            propNames += exp.PropertyName + ".";
                        }
                    }

                    return module.Negotiate.RespondWithValidationFailure("Model binding failed: " + propNames);
                }

                return handler(model);
            }
            catch (HttpException hEx)
            {
                return module.Negotiate.WithStatusCode(hEx.StatusCode).WithModel(hEx.Content);
            }
        }

        public static async Task<object> RunHandlerAsync<TIn, TOut>(this NancyModule module, Func<TIn, Task<TOut>> handler)
        {
            try
            {
                TIn model;
                try
                {
                    model = module.BindAndValidate<TIn>();
                    if (!module.ModelValidationResult.IsValid)
                    {
                        return module.Negotiate.RespondWithValidationFailure(module.ModelValidationResult);
                    }
                }
                catch (ModelBindingException ex)
                {
                    return module.Negotiate.RespondWithValidationFailure("Model binding failed:" + ex.Message);
                }

                TOut result = await handler(model);
                return result;
            }
            catch (HttpException hEx)
            {
                return module.Negotiate.WithStatusCode(hEx.StatusCode).WithModel(hEx.Content);
            }
        }

        public static Negotiator RespondWithValidationFailure(this Negotiator negotiate, ModelValidationResult validationResult)
        {
            var model = new ValidationFailedResponse(validationResult);

            return negotiate
                .WithModel(model)
                .WithAllowedMediaRange(new MediaRange("application/json"))
                .WithStatusCode(HttpStatusCode.BadRequest);
        }

        public static object RespondWithValidationFailure(this Negotiator negotiate, string message)
        {
            // var model = new ValidationFailedResponse(message);
            HttpStatusCode httpCode = HttpStatusCode.BadRequest;
            var model = new RspFrame { code = (int)httpCode, msg = message };

            return negotiate
                .WithModel(model)
                .WithAllowedMediaRange(new MediaRange("application/json"))
                .WithStatusCode(HttpStatusCode.BadRequest);
        }
    }
}