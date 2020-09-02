namespace BattleshipShared.Enums
{
    /// <summary>
    /// The position of ship, inform what exacly orientation on board ship will placed
    /// </summary>
    public enum LocationType
    {
        /// <summary>
        /// Default value of empty instance of enum
        /// </summary>
        None = 0,

        /// <summary>
        /// Ship will be placed horizontaly from right to left on board
        /// </summary>
        HorizontalToLeft = 1,

        /// <summary>
        /// Ship will be placed horizontaly from left to right on board
        /// </summary>
        HorizontalToRight = 2,

        /// <summary>
        /// Ship will be placed verticaly from bottom to top on board
        /// </summary>
        VerticalToTop = 3,

        /// <summary>
        /// Ship will be placed verticaly from top to bottom on board
        /// </summary>
        VerticalToBottom = 4
    }
}
