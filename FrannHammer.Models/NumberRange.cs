namespace FrannHammer.Models
{
    /// <summary>
    /// Used to hold the specific values of a retrieved hitbox or metadata for a move.
    ///  E.g., Start = 4, End = 10 for "4-10" hitbox
    /// </summary>
    public class NumberRange
    {
        public int Start { get; }
        public int? End { get; }

        public NumberRange(int start, int end = -1)
        {
            Start = start;
            if (end > 0)
            {
                End = end;
            }
        }
    }
}
