namespace RestaurantOrderAPI.Utilities
{
    /// <summary>
    /// Represents the type of a dish
    /// </summary>
    public enum DishType
    {
        /// <summary>
        /// Indicates that the type 
        /// of the dish is not specified
        /// </summary>
        NoType = 0,
        /// <summary>
        /// Indicates that the dish 
        /// is a first course
        /// </summary>
        FirstCourse = 1,
        /// <summary>
        /// Indicates that the dish 
        /// is a second course
        /// </summary>
        SecondCourse = 2,
        /// <summary>
        /// Indicates that the dish 
        /// is a side dish
        /// </summary>
        SideDish = 3,
        /// <summary>
        /// Indicates that the dish 
        /// is a dessert
        /// </summary>
        Dessert = 4
    }
}
