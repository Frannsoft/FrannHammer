using System;
using System.Linq;
using System.Text.RegularExpressions;
using FrannHammer.Models;

namespace KuroganeHammer.TransferDBTool.Seeding
{
    internal class TransferMethods
    {
        internal static T MapDataThenSync<T>(Move move, AppDbContext context)
            where T : BaseMeta
        {
            if (typeof(T) == typeof(Hitbox))
            {
                return (T)(object)AddHitboxDataToHitboxTable(move, context);
            }
            if (typeof(T) == typeof(BaseKnockback))
            {
                return (T)(object)AddBaseKnockback_DataTo_BaseKnockback_Table(move, context);
            }
            if (typeof(T) == typeof(SetKnockback))
            {
                return (T)(object)AddSetKnockback_DataTo_SetKnockback_Table(move, context);
            }
            if (typeof(T) == typeof(BaseDamage))
            {
                return (T)(object)AddBaseDamageDataToTable(move, context);
            }
            if (typeof(T) == typeof(Angle))
            {
                return (T)(object)AddAngleDataToTable(move, context);
            }
            if (typeof(T) == typeof(Autocancel))
            {
                return (T)(object)AddAutocancelDataToTable(move, context);
            }
            if (typeof(T) == typeof(LandingLag))
            {
                return (T)(object)AddLandingLagDataToTable(move, context);
            }
            if (typeof(T) == typeof(KnockbackGrowth))
            {
                return (T)(object)AddKnockbackGrowthDataToTable(move, context);
            }
            throw new Exception("No applicable type found");
        }

        internal static KnockbackGrowth AddKnockbackGrowthDataToTable(Move move, AppDbContext context)
        {
            var knockbackGrowth = SetBaseHitboxData<KnockbackGrowth>(move.KnockbackGrowth, '/', move, context);
            return knockbackGrowth;
        }

        private static void SetHitboxesData<T>(T model, string[] rawData)
            where T : BaseMoveHitboxMeta
        {
            if (rawData.Length > 0)
            {
                string hitboxActiveNoParens = SeparateNotesDataFromHitbox(model, rawData[0]);
                model.Hitbox1 = hitboxActiveNoParens;
            }

            if (rawData.Length > 1)
            {
                string hitboxActiveNoParens = SeparateNotesDataFromHitbox(model, rawData[1]);
                model.Hitbox2 = hitboxActiveNoParens;
            }

            if (rawData.Length > 2)
            {
                string hitboxActiveNoParens = SeparateNotesDataFromHitbox(model, rawData[2]);
                model.Hitbox3 = hitboxActiveNoParens;
            }

            if (rawData.Length > 3)
            {
                string hitboxActiveNoParens = SeparateNotesDataFromHitbox(model, rawData[3]);
                model.Hitbox4 = hitboxActiveNoParens;
            }

            if (rawData.Length > 4)
            {
                string hitboxActiveNoParens = SeparateNotesDataFromHitbox(model, rawData[4]);
                model.Hitbox5 = hitboxActiveNoParens;
            }
        }

        private static string SeparateNotesDataFromHitbox<T>(T model, string rawData)
            where T : BaseMoveHitboxMeta
        {
            string noteData = GetNoteDataFromHitboxActiveData(rawData);
            model.Notes += noteData;
            return !string.IsNullOrEmpty(noteData) ? rawData.Replace(noteData, string.Empty).Trim() : rawData;
        }

        private static string GetNoteDataFromHitboxActiveData(string rawHitboxData)
        {
            var match = Regex.Match(rawHitboxData, @"\(([^\)]+)\)");
            return match.Value;
        }

        private static T SetBaseHitboxData<T>(string rawValue, char splitOn, Move move, AppDbContext context)
            where T : BaseMoveHitboxMeta
        {
            var splitData = rawValue.Split(splitOn);

            var retVal = (T)Activator.CreateInstance(typeof(T));
            retVal.RawValue = rawValue; //hard copy of data prior to parsing
            retVal.Owner = context.Characters.Single(c => c.Id == move.OwnerId);
            retVal.OwnerId = context.Characters.Single(c => c.Id == move.OwnerId).Id;
            retVal.LastModified = DateTime.Now;
            retVal.Move = move;
            retVal.MoveId = move.Id;

            if (!rawValue.Contains("No") &&
                !rawValue.Contains("Yes"))
            {
                SetHitboxesData(retVal, splitData);
            }

            return retVal;
        }

        internal static Angle AddAngleDataToTable(Move move, AppDbContext context)
        {
            var angle = SetBaseHitboxData<Angle>(move.Angle, '/', move, context);
            return angle;
        }

        private static LandingLag AddLandingLagDataToTable(Move move, AppDbContext context)
        {
            var rawData = move.LandingLag;

            int frames;

            if (!int.TryParse(rawData, out frames))
            {
                frames = 0;
            }


            var landingLag = new LandingLag
            {
                LastModified = DateTime.Now,
                Owner = context.Characters.Single(c => c.Id == move.OwnerId),
                OwnerId = context.Characters.Single(c => c.Id == move.OwnerId).Id,
                Move = move,
                MoveId = move.Id,
                Frames = frames,
                RawValue = rawData
            };

            return landingLag;
        }

        private static Autocancel AddAutocancelDataToTable(Move move, AppDbContext context)
        {
            //I don't want to ref System.Web (for httputility) just for this call.
            string rawData = move.AutoCancel.Replace("&gt;", ">");

            var splitData = rawData.Split(',');

            var autocancel = new Autocancel
            {
                LastModified = DateTime.Now,
                Owner = context.Characters.Single(c => c.Id == move.OwnerId),
                OwnerId = context.Characters.Single(c => c.Id == move.OwnerId).Id,
                Move = move,
                MoveId = move.Id,
                RawValue = rawData
            };

            if (splitData.Length > 0)
            {
                autocancel.Cancel1 = splitData[0];
            }
            if (splitData.Length > 1)
            {
                autocancel.Cancel2 = splitData[1];
            }

            return autocancel;
        }

        internal static BaseDamage AddBaseDamageDataToTable(Move move, AppDbContext context)
        {
            var baseDamage = SetBaseHitboxData<BaseDamage>(move.BaseDamage, '/', move, context);
            return baseDamage;
        }

        private static T AddBaseSetKnockbackCore<T>(string rawKbk, string regexPattern, char[] trimValues,
            char primaryKnockbackValueChar, Func<string, bool> doesRawContainKnockBackValue,
            Move move, AppDbContext context)
            where T : BaseMoveHitboxMeta
        {
            const char splitValue = '/';

            if (rawKbk.Contains(primaryKnockbackValueChar))
            {
                var baseKbksRegexMatch = Regex.Match(rawKbk, regexPattern).Value;
                var trimmed = baseKbksRegexMatch.Trim(trimValues);
                return string.IsNullOrEmpty(trimmed) ?
                        null :
                        SetBaseHitboxData<T>(trimmed, splitValue, move, context);
            }
            if (doesRawContainKnockBackValue(rawKbk))
            {
                return null;
            }

            return rawKbk.Equals("-") || string.IsNullOrEmpty(rawKbk) ?
                null :
                SetBaseHitboxData<T>(rawKbk, splitValue, move, context);
        }

        internal static BaseKnockback AddBaseKnockback_DataTo_BaseKnockback_Table(
            Move move, AppDbContext context)
        {
            var rawKbk = move.BaseKnockBackSetKnockback;
            const string matchBaseKnockbackRegex = "(B: ).[^W]*";
            char[] baseKnockbackTrimValues = { 'B', ':', ' ' };
            const char primaryKnockbackValueChar = 'B';
            Func<string, bool> doesRawContainBaseKnockbackValue = raw => raw.Contains('W') && !raw.Contains('B');

            return AddBaseSetKnockbackCore<BaseKnockback>(rawKbk, matchBaseKnockbackRegex, baseKnockbackTrimValues,
                primaryKnockbackValueChar,
                doesRawContainBaseKnockbackValue, move, context);
        }

        internal static SetKnockback AddSetKnockback_DataTo_SetKnockback_Table(
            Move move, AppDbContext context)
        {
            var rawKbk = move.BaseKnockBackSetKnockback;
            const string matchSetKnockbackRegex = "(W: ).[^B]*";
            char[] setKnockbackTrimValues = { 'W', ':', ' ' };
            const char primaryKnockbackValueChar = 'W';
            Func<string, bool> doesRawContainSetKnockbackValue = raw => rawKbk.Contains('B') &&
                                                                        !rawKbk.Contains('W') ||
                                                                        !rawKbk.Contains('B') &&
                                                                        !rawKbk.Contains('W');

            return AddBaseSetKnockbackCore<SetKnockback>(rawKbk, matchSetKnockbackRegex, setKnockbackTrimValues,
                primaryKnockbackValueChar,
                doesRawContainSetKnockbackValue, move, context);
        }

        //private static BaseKnockbackSetKnockback AddBaseKnockbackSetKnockback_DataTo_BaseKnockbackSetKnockback_Table(
        //    Move move, AppDbContext context)
        //{
        //    var kbk = SetBaseHitboxData<BaseKnockbackSetKnockback>(move.BaseKnockBackSetKnockback, '/', move, context);
        //    return kbk;
        //}

        private static Hitbox AddHitboxDataToHitboxTable(Move move, AppDbContext context)
        {
            var hitbox = SetBaseHitboxData<Hitbox>(move.HitboxActive, ',', move, context);
            return hitbox;
        }
    }
}
