namespace Sample2015.Web.Helper.Extensions.ModelValidation
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Responses.Negotiation;
    using Nancy.Validation;

    public class ValidationFailedResponse
    {
        public ValidationFailedResponse()
        {
        }

        public ValidationFailedResponse(ModelValidationResult validationResult)
        {
            this.Messages = new List<string>();
            this.ErrorsToStrings(validationResult);
        }

        public ValidationFailedResponse(string message)
        {
            this.Messages = new List<string>
            {
                message
            };
        }

        public List<string> Messages { get; set; }

        private void ErrorsToStrings(ModelValidationResult validationResult)
        {
            foreach (var errorGroup in validationResult.Errors)
            {
                foreach (var error in errorGroup.Value)
                {
                    this.Messages.Add(error.ErrorMessage);
                }
            }
        }
    }
}