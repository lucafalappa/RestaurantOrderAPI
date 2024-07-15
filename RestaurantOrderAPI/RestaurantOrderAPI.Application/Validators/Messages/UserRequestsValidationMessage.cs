namespace RestaurantOrderAPI.Application.Validators.Messages
{
    /// <summary>
    /// Provides constant validation messages for user requests
    /// </summary>
    public static class UserRequestsValidationMessage
    {
        /// <summary>
        /// The validation message for email field
        /// </summary>
        public const string EmailMessage = "Starts with one or more alphanumeric characters, " +
                "dots (.), percent signs (%), plus signs (+), or hyphens (-)." +
                " Followed by the at symbol (@) and one or more alphanumeric characters, " +
                "dots (.), or hyphens (-)." +
                " Ends with a dot (.) followed by two or more alphabetic characters";
        /// <summary>
        /// The validation message for password field
        /// </summary>
        public const string PasswordMessage = "Contains at least one uppercase letter" +
                ", contains at least one lowercase letter" +
                ", contains at least one digit" +
                ", contains at least one non alphanumeric character" +
                ", has a length of 8 or more characters";
    }
}
