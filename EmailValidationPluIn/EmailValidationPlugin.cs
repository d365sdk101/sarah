using Microsoft.Xrm.Sdk;
using System;

namespace SarahTraining.Plugin
{
    public class EmailValidationPlugin:IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            // Obtain the execution context from the service provider.
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            // Check if the message is a create or update of the target entity.
            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity targetEntity)
            {
                // Check if the target entity contains the email attribute.
                if (targetEntity.Contains("emailaddress1"))
                {
                    // Get the email attribute value from the target entity.
                    string email = targetEntity.GetAttributeValue<string>("emailaddress1");
                    // Validate the email address using a regular expression.
                    if (!IsValidEmail(email))
                    {
                        throw new InvalidPluginExecutionException("Invalid email address format. Please provide a valid email address.");
                    }
                }
            }
        }

        private bool IsValidEmail(string email)
        {
            // You can use a regular expression to validate the email address.
            // Here's a simple example, but you may use a more comprehensive pattern as needed.
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);
            //end
        }
    }

}

