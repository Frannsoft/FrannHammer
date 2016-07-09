namespace FrannHammer.Models
{
    ///// <summary>
    ///// Base model for making
    ///// </summary>
    //public class CalculatorRequestModel
    //{
    //    public double AttackerPercent { get; set; }
    //    public double VictimPercent { get; set; }
    //    public int BaseDamage { get; set; }
    //    public double TargetWeight { get; set; }
    //    public double KnockbackGrowth { get; set; }
    //    public int BaseKnockbackSetKnockback { get; set; }
    //    public double HitlagModifier { get; set; }
    //    public int HitFrame { get; set; }
    //    public int FirstActiveFrame { get; set; }
    //    public double Staleness { get; set; }
    //    public Modifiers Modifiers { get; set; }
    //    public ElectricModifier ElectricModifier { get; set; }
    //    public CrouchingModifier CrouchingModifier { get; set; }
    //    public ShieldAdvantageModifier ShieldAdvantageModifier { get; set; }
    //}



    //public class CalculatorResponseModel
    //{
    //    public double Rage { get; set; }
    //    public double TrainingModeKnockback { get; set; }
    //    public double VersusModeKnockback { get; set; }
    //    public int HitstunFrames { get; set; }
    //    public int HitlagFrames { get; set; }
    //    public int ShieldAdvantage_Normal { get; set; }
    //    public int ShieldAdvantage_Powershield { get; set; }
    //    public int ShieldAdvantage_Projectile { get; set; }
    //    public int ShieldAdvantage_PowershieldProjectile { get; set; }
    //    public int ShieldStun_Normal { get; set; }
    //    public int ShieldStun_Powershield { get; set; }
    //    public int ShieldStun_Projectile { get; set; }
    //    public int ShieldStun_PowershieldProjectile { get; set; }
    //}

    /// <summary>
    /// Model used for performing calculations on character moves.
    /// </summary>
    public class CalculatorMoveModel 
    {
        /// <summary>
        /// The Id of the Move in question.
        /// </summary>
        public int MoveId { get; set; }

        /// <summary>
        /// The damage percent the attacker has.
        /// </summary>
        public int AttackerDamagePercent { get; set; }

        /// <summary>
        /// The damage percent the target being hit has.
        /// </summary>
        public int VictimDamagePercent { get; set; }

        /// <summary>
        /// The weight of the target.
        /// </summary>
        public int TargetWeight { get; set; }

        /// <summary>
        /// The stale move negation level.  More details on this found at: http://kuroganehammer.com/Smash4/Formulas.  
        /// Default in no staleness on a move.
        /// </summary>
        public StaleMoveNegationMultipler StaleMoveNegationMultiplier { get; set; } = StaleMoveNegationMultipler.S1;

        /// <summary>
        /// What type of attack is being performed.  More details on this found at: http://kuroganehammer.com/Smash4/Formulas.   
        /// </summary>
        public ElectricModifier ElectricModifier { get; set; } = ElectricModifier.NormalAttack;

        /// <summary>
        /// Whether or not the target being hit is crouching or otherwise.  More details on this found at: http://kuroganehammer.com/Smash4/Formulas.  
        /// </summary>
        public CrouchingModifier CrouchingModifier { get; set; } = CrouchingModifier.NotCrouching;

        /// <summary>
        /// Shield advantage modifier based on normal or projectile.  More details on this found at: http://kuroganehammer.com/Smash4/Formulas.  
        /// </summary>
        public ShieldAdvantageModifier ShieldAdvantageModifier { get; set; } = ShieldAdvantageModifier.Regular;

        /// <summary>
        /// Whether or not the victim is doing something other than standing when hit. More details on this found at: http://kuroganehammer.com/Smash4/Formulas.  
        /// </summary>
        public Modifiers Modifiers { get; set; } = Modifiers.Standing;

        /// <summary>
        /// The specific hitbox frames for this moves.  Since some moves have multiple frames with multiple hitboxes, specify which hitbox data should be 
        /// used.  
        /// </summary>
        public HitboxOptions HitboxOption { get; set; } = HitboxOptions.First;
    }

}