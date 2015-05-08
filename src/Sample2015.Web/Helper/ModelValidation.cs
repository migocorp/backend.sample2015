namespace Sample2015.Web.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Web;
    using Nancy;
    using Nancy.Validation;

    public class ModelValidation
    {
        public static DynamicDictionary WrongValidationModel(dynamic data, IList<string> messages, INancyModule module)
        {
            dynamic model = new DynamicDictionary();

            model.Data = data;
            ////model.UserRole = module.GetCurrentUser().AccountRole.Min(r => r.ID);

            // must be ExpandoObject object
            dynamic validation = new ExpandoObject();
            validation.IsValid = false;
            validation.Messages = messages;
            model.Error = validation;

            return model;
        }

        public static DynamicDictionary RightValidationModel(dynamic data, INancyModule module)
        {
            dynamic model = new DynamicDictionary();

            model.Data = data;
            ////model.UserRole = module.GetCurrentUser().AccountRole.Min(r => r.ID);

            dynamic validation = new DynamicDictionary();
            validation.IsValid = true;
            model.Error = validation;

            return model;
        }

        public static IList<string> Validate<T>(NancyModule module, T req)
        {
            var result = module.Validate<T>(req);
            if (!result.IsValid)
            {
                IList<string> messages = new List<string>();
                foreach (var key in result.Errors.Keys)
                {
                    foreach (var value in result.Errors[key])
                    {
                        messages.Add(value);
                    }
                }

                return messages;
            }

            return null;
        }

        public static IList<string> ValidatePassword(string password, string password_check)
        {
            if (!password.Equals(password_check))
            {
                IList<string> messages = new List<string>();
                messages.Add("密碼輸入不相同");

                return messages;
            }

            return null;
        }
    }
}