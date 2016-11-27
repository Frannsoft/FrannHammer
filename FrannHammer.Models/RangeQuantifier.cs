namespace FrannHammer.Models
{
    /// <summary>
    /// Used when determining how to compare move attributes while searching through them.
    /// </summary>
    public enum RangeQuantifier
    {
        /// <summary>
        /// Represents that a <see cref="RangeModel"/>s <see cref="RangeModel.StartValue"/>
        /// is the exact same as the one represented by the <see cref="Move"/> attribute being
        /// compared.
        /// 
        /// Flag = 0.
        /// </summary>
        EqualTo,

        /// <summary>
        /// Represents that a <see cref="RangeModel"/>s <see cref="RangeModel.StartValue"/>
        /// is greater than the <see cref="Move"/> attribute being compared in the database.
        /// 
        /// Flag = 1.
        /// </summary>
        GreaterThan,

        /// <summary>
        /// Represents that a <see cref="RangeModel"/>s <see cref="RangeModel.StartValue"/>
        /// is less than the <see cref="Move"/> attribut being compared in the database.
        /// 
        /// Flag = 2.
        /// </summary>
        LessThan,

        /// <summary>
        /// Represents that a <see cref="RangeModel"/>s <see cref="RangeModel.StartValue"/>
        /// is greater than OR equal to the <see cref="Move"/> attribute being compared in the 
        /// database.
        /// 
        /// Flag = 3.
        /// </summary>
        GreaterThanOrEqualTo,

        /// <summary>
        /// Represents that a <see cref="RangeModel"/>s <see cref="RangeModel.StartValue"/>
        /// is less than OR equal to the <see cref="Move"/> attribute being compared in the 
        /// database.
        /// 
        /// Flag = 4.
        /// </summary>
        LessThanOrEqualTo,

        /// <summary>
        /// Represents that a match for <see cref="Move"/> attributes must be inbetween the
        /// accompanying <see cref="RangeModel"/>s <see cref="RangeModel.StartValue"/> and
        /// <see cref="RangeModel.EndValue"/>.  
        /// 
        /// NOTE:  <see cref="Move"/> attribute data that is equal to either the <see cref="RangeModel.StartValue"/>
        /// or <see cref="RangeModel.EndValue"/> is not matched in this case.
        /// 
        /// Flag = 5.
        /// </summary>
        Between
    }
}
