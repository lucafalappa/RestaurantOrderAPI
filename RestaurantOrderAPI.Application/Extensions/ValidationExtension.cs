using FluentValidation;
using System.Text.RegularExpressions;

namespace RestaurantOrderAPI.Application.Extensions
{
    public static class ValidationExtension
    {
        public static void IsOnlyLetters<T, TProperty>
            (this IRuleBuilderOptions<T, TProperty> ruleBuilder, 
            string validationMessage)
        {
            ruleBuilder.Custom((value, context) =>
            {
                //TODO : MAYBE NOT SO EFFICIENT
                if (!value.ToString().All(char.IsLetter))
                {
                    context.AddFailure(validationMessage);
                }
            });
        }
        public static void IsDishTypeValid<T, TProperty>
            (this IRuleBuilderOptions<T, TProperty> ruleBuilder, 
            string validationMessage)
        {
            var allowed = new int[]{ 0, 1, 2, 3, 4 };
            ruleBuilder.Custom((value, context) =>
            {
                int number;
                if (!int.TryParse(value.ToString(), out number))
                {
                    context.AddFailure(validationMessage);
                } else
                {
                    if (!allowed.Contains(number))
                    {
                        context.AddFailure(validationMessage);
                    }
                }
            });
        }
        public static void IsGreaterThan<T, TProperty>
            (this IRuleBuilderOptions<T, TProperty> ruleBuilder, 
            DateTime startDate, string validationMessage)
        {
            ruleBuilder.Custom((value, context) =>
            {
                DateTime endDate;
                if (!DateTime.TryParse(value.ToString(), out endDate))
                {
                    context.AddFailure(validationMessage);
                } else
                {
                    if (startDate > endDate)
                    {
                        context.AddFailure(validationMessage);
                    }
                }
            });
        }
        public static void IsCorrectEmail<T, TProperty>
            (this IRuleBuilderOptions<T, TProperty> ruleBuilder, 
            string validationMessage)
        {
            ruleBuilder.Custom((value, context) =>
            {
                Regex regex = new Regex
                    (@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
                if (!regex.IsMatch(value.ToString())) 
                    { context.AddFailure(validationMessage); }
            });
        }
        public static void IsCorrectPassword<T, TProperty>
            (this IRuleBuilderOptions<T, TProperty> ruleBuilder,
            string validationMessage)
        {
            ruleBuilder.Custom((value, context) =>
            {
                Regex regex = new Regex
                    ("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)" +
                    "(?=.*[!@#$%^&*()_+{}\\[\\]:;<>,.?~\\\\-]).{8,}$");
                if (!regex.IsMatch(value.ToString()))
                { context.AddFailure(validationMessage); }
            });
        }
        public static void IsCorrectRole<T, TProperty>
            (this IRuleBuilderOptions<T, TProperty> ruleBuilder,
            string validationMessage)
        {
            var admissibleRoles = new string[] 
                { "NOROLE", "ADMINISTRATOR", "CUSTOMER" };
            ruleBuilder.Custom((value, context) =>
            {
                if (!(admissibleRoles.Contains(value.ToString().ToUpper())))
                {
                    context.AddFailure(validationMessage);
                }
            });
        }
    }
}
