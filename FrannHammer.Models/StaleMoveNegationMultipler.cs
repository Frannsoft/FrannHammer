using System;

namespace FrannHammer.Models
{
    /// <summary>
    /// The staleness of a move.  The higher the level the more stale the move is.
    /// </summary>
    [Obsolete]
    public enum StaleMoveNegationMultipler
    {
        /// <summary>
        /// S1 = 8.0
        /// </summary>
        [ModifierValue(8.000)]
        S1,

        /// <summary>
        /// S2 = 7.594
        /// </summary>
        [ModifierValue(7.594)]
        S2,

        /// <summary>
        /// S3 = 6.782
        /// </summary>
        [ModifierValue(6.782)]
        S3,

        /// <summary>
        /// S4 = 6.028
        /// </summary>
        [ModifierValue(6.028)]
        S4,

        /// <summary>
        /// S5 = 5.274
        /// </summary>
        [ModifierValue(5.274)]
        S5,

        /// <summary>
        /// S6 = 4.462
        /// </summary>
        [ModifierValue(4.462)]
        S6,

        /// <summary>
        /// S7 = 3.766
        /// </summary>
        [ModifierValue(3.766)]
        S7,

        /// <summary>
        /// S8 = 2.954
        /// </summary>
        [ModifierValue(2.954)]
        S8,

        /// <summary>
        /// S9 = 2.2
        /// </summary>
        [ModifierValue(2.200)]
        S9
    }
}