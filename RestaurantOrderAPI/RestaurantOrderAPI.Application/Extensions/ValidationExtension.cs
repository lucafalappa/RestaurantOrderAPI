using FluentValidation;
using System.Text.RegularExpressions;

namespace RestaurantOrderAPI.Application.Extensions
{
    /// <summary>
    /// Extension methods for FluentValidation rules
    /// </summary>
    public static class ValidationExtension
    {
        /// <summary>
        /// Validates a string that contains only letters,
        /// digits, punctuations or symbols
        /// </summary>
        /// <typeparam name="T">The type of the 
        /// object being validated
        /// </typeparam>
        /// <typeparam name="TProperty">The type of the 
        /// property being validated
        /// </typeparam>
        /// <param name="ruleBuilder">The rule builder
        /// </param>
        /// <param name="validationMessage">The validation message 
        /// to use when validation fails
        /// </param>
        public static void IsOnlyLetters<T, TProperty>
            (this IRuleBuilderOptions<T, TProperty> ruleBuilder,
            string validationMessage)
        {
            ruleBuilder.Custom((value, context) =>
            {
                var valueString = value.ToString();
                for (int i = 0; i < valueString.Length; i++)
                {
                    if (!char.IsLetterOrDigit(valueString[i])
                        && !char.IsPunctuation(valueString[i])
                        && !char.IsSymbol(valueString[i]))
                    {
                        context.AddFailure(validationMessage);
                        break;
                    }
                }
            });
        }
        /// <summary>
        /// Validates a number that represents a valid DishType
        /// </summary>
        /// <typeparam name="T">The type of the 
        /// object being validated
        /// </typeparam>
        /// <typeparam name="TProperty">The type of the 
        /// property being validated
        /// </typeparam>
        /// <param name="ruleBuilder">The rule builder
        /// </param>
        /// <param name="validationMessage">The validation message 
        /// to use when validation fails
        /// </param>
        public static void IsDishTypeValid<T, TProperty>
            (this IRuleBuilderOptions<T, TProperty> ruleBuilder,
            string validationMessage)
        {
            var allowed = new int[] { 1, 2, 3, 4 };
            ruleBuilder.Custom((value, context) =>
            {
                int number;
                if (!int.TryParse(value.ToString(), out number))
                {
                    context.AddFailure(validationMessage);
                }
                else
                {
                    if (!allowed.Contains(number))
                    {
                        context.AddFailure(validationMessage);
                    }
                }
            });
        }
        /// <summary>
        /// Validates a DateTime that is greater than
        /// a specified start date
        /// </summary>
        /// <typeparam name="T">The type of the 
        /// object being validated
        /// </typeparam>
        /// <typeparam name="TProperty">The type of the 
        /// property being validated
        /// </typeparam>
        /// <param name="ruleBuilder">The rule builder
        /// </param>
        /// <param name="validationMessage">The validation message 
        /// to use when validation fails
        /// </param>
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
                }
                else
                {
                    if (startDate > endDate)
                    {
                        context.AddFailure(validationMessage);
                    }
                }
            });
        }
        /// <summary>
        /// Validates a string that is a correct email format
        /// </summary>
        /// <typeparam name="T">The type of the 
        /// object being validated
        /// </typeparam>
        /// <typeparam name="TProperty">The type of the 
        /// property being validated
        /// </typeparam>
        /// <param name="ruleBuilder">The rule builder
        /// </param>
        /// <param name="validationMessage">The validation message 
        /// to use when validation fails
        /// </param>
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
        /// <summary>
        /// Validates a string that is a correct password format
        /// </summary>
        /// <typeparam name="T">The type of the 
        /// object being validated
        /// </typeparam>
        /// <typeparam name="TProperty">The type of the 
        /// property being validated
        /// </typeparam>
        /// <param name="ruleBuilder">The rule builder
        /// </param>
        /// <param name="validationMessage">The validation message 
        /// to use when validation fails
        /// </param>
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
        /// <summary>
        /// Validates a string that is a correct UserRole format
        /// </summary>
        /// <typeparam name="T">The type of the 
        /// object being validated
        /// </typeparam>
        /// <typeparam name="TProperty">The type of the 
        /// property being validated
        /// </typeparam>
        /// <param name="ruleBuilder">The rule builder
        /// </param>
        /// <param name="validationMessage">The validation message 
        /// to use when validation fails
        /// </param>
        public static void IsCorrectRole<T, TProperty>
            (this IRuleBuilderOptions<T, TProperty> ruleBuilder,
            string validationMessage)
        {
            var admissibleRoles = new string[]
                { "ADMINISTRATOR", "CUSTOMER" };
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
