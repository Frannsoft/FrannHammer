using System;
using System.Linq;
using System.Collections.Generic;

namespace Kurogane.Data.RestApi.Models
{
    public class MoveStat : Stat
    {
        public string HitboxActive { get; set; }
        public int TotalHitboxActiveLength { get; set; }
        public int FirstActionableFrame { get; set; }
        public string BaseDamage { get; set; }
        public string Angle { get; set; }
        public string BaseKnockBackSetKnockback { get; set; }
        public string LandingLag { get; set; }
        public string AutoCancel { get; set; }
        public string KnockbackGrowth { get; set; }
        public MoveType Type { get; set; }

        public MoveStat(string name, int ownerId, string hitboxActive, string firstActionableFrame, string baseDamage,
            string angle, string baseKnockbackSetKnockback, string knockbackGrowth, string landingLag = "", string autoCancel = "")
            : base(name, ownerId)
        {
            HitboxActive = hitboxActive;
            //TotalHitboxActiveLength = DetermineHitBoxActiveLength(hitboxActive);
            firstActionableFrame = firstActionableFrame.Replace(" ", string.Empty);

            int result = 0;
            if (int.TryParse(firstActionableFrame, out result))
            {
                FirstActionableFrame = Convert.ToInt32(result);
            }

            BaseDamage = baseDamage;
            Angle = angle;
            BaseKnockBackSetKnockback = baseKnockbackSetKnockback;
            KnockbackGrowth = knockbackGrowth;
            LandingLag = landingLag;
            AutoCancel = autoCancel;
        }

        public MoveStat()
        { }

        private int DetermineHitBoxActiveLength(string hitBoxActiveString)
        {
            List<int> ints = new List<int>();
            string[] vals;
            if (hitBoxActiveString.Contains(','))
            {
                vals = hitBoxActiveString.Split(new char[] { ',' });

                foreach (var commaVal in vals)
                {
                    AddHitboxLength(ints, commaVal.Split(new char[] { '-' }, 2));
                }
            }
            else
            {
                AddHitboxLength(ints, hitBoxActiveString.Split(new char[] { '-' }, 2));
            }

            int total = ints.Aggregate(0, (i, j) => i + j);
            return total;
        }

        private void AddHitboxLength(List<int> ints, string[] vals)
        {
            int result = GetDifference(vals);
            if (result > 0)
            {
                ints.Add(result);
            }
        }

        private int GetDifference(string[] vals)
        {
            int one, two, result;

            if (vals.Length > 1)
            {
                one = Convert.ToInt32(vals[0]);
                two = Convert.ToInt32(vals[1]);
                result = two - one;
            }
            else
            {
                result = 1; //add one
            }


            return result;
        }
    }



}
