using System;
using System.Configuration;
using System.Linq;
using KuroganeHammer.Data.Api.Models;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (AppDbContext context = new AppDbContext())
            {
                SyncData<Hitbox>(context);
                SyncData<BaseKnockbackSetKnockback>(context);
                SyncData<BaseDamage>(context);
                SyncData<Angle>(context);
                SyncData<Autocancel>(context);
                SyncData<LandingLag>(context);
                SyncData<KnockbackGrowth>(context);
            }
        }

        private static void SyncData<T>(AppDbContext context)
            where T : BaseMeta
        {
            if (!context.Set<T>().Any())
            {
                var moves = context.Moves.ToList();

                foreach (var move in moves)
                {
                    var metaData = TransferMethods.MapDataThenSync<T>(move, context);
                    Console.WriteLine($"Adding {metaData.GetType().Name} data for moveId: {move.Id}");
                    context.Set<T>().Add(metaData);
                }
                context.SaveChanges();
            }
        }

        //private static void SyncHitboxData(AppDbContext context)
        //{
        //    if (!context.Hitbox.Any())
        //    {
        //        var moves = context.Moves.ToList();

        //        foreach (var move in moves)
        //        {
        //            var hitbox = TransferMethods.AddHitboxDataToHitboxTable(move, context);
        //            Console.WriteLine($"Adding Hitbox data for moveId: {move.Id}");
        //            context.Hitbox.Add(hitbox);
        //        }
        //        context.SaveChanges();
        //    }
        //}

        //private static void SyncBaseKnockbackSetKnockbackData(AppDbContext context)
        //{
        //    if (!context.BaseKnockbackSetKnockback.Any())
        //    {
        //        var moves = context.Moves.ToList();

        //        foreach (var move in moves)
        //        {
        //            var kbk = TransferMethods.AddBaseKnockbackSetKnockback_DataTo_BaseKnockbackSetKnockback_Table(move,
        //                context);
        //            Console.WriteLine($"Adding BaseKnockbackSetKnockback data for moveId: {move.Id}");
        //            context.BaseKnockbackSetKnockback.Add(kbk);
        //        }
        //        context.SaveChanges();
        //    }
        //}
    }

    public class AppDbContext : ApplicationDbContext
    {
        public AppDbContext()
            : base(ConfigurationManager.AppSettings["connection"])
        { }
    }

    public class TransferMethods
    {
        internal static T MapDataThenSync<T>(Move move, AppDbContext context)
            where T : BaseMeta
        {
            if (typeof(T) == typeof(Hitbox))
            {
                return (T)(object)AddHitboxDataToHitboxTable(move, context);
            }
            if (typeof(T) == typeof(BaseKnockbackSetKnockback))
            {
                return (T)(object)AddBaseKnockbackSetKnockback_DataTo_BaseKnockbackSetKnockback_Table(move, context);
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
            if (typeof (T) == typeof (LandingLag))
            {
                return (T) (object) AddLandingLagDataToTable(move, context);
            }
            if (typeof (T) == typeof (KnockbackGrowth))
            {
                return (T) (object) AddKnockbackGrowthDataToTable(move, context);
            }
            throw new Exception("No applicable type found");
        }

        private static KnockbackGrowth AddKnockbackGrowthDataToTable(Move move, AppDbContext context)
        {
            var knockbackGrowth = SetBaseHitboxData<KnockbackGrowth>(move.KnockbackGrowth, '/', move, context);
            return knockbackGrowth;
        }

        private static void SetHitboxesData<T>(T model, string[] rawData)
            where T : BaseMoveHitboxMeta
        {
            if (rawData.Length > 0)
            {
                model.Hitbox1 = rawData[0];
            }

            if (rawData.Length > 1)
            {
                model.Hitbox2 = rawData[1];
            }

            if (rawData.Length > 2)
            {
                model.Hitbox3 = rawData[2];
            }

            if (rawData.Length > 3)
            {
                model.Hitbox4 = rawData[3];
            }

            if (rawData.Length > 4)
            {
                model.Hitbox5 = rawData[4];
            }
        }

        private static T SetBaseHitboxData<T>(string rawValue, char splitOn, Move move, AppDbContext context)
            where T : BaseMoveHitboxMeta
        {
            var splitData = rawValue.Split(splitOn);

            var retVal = (T)Activator.CreateInstance(typeof(T));
            retVal.Character = context.Characters.Single(c => c.Id == move.OwnerId);
            retVal.CharacterId = context.Characters.Single(c => c.Id == move.OwnerId).Id;
            retVal.LastModified = DateTime.Now;
            retVal.Move = move;
            retVal.MoveId = move.Id;

            SetHitboxesData(retVal, splitData);

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
                Character = context.Characters.Single(c => c.Id == move.OwnerId),
                CharacterId = context.Characters.Single(c => c.Id == move.OwnerId).Id,
                Move = move,
                MoveId = move.Id,
                Frames = frames
            };

            return landingLag;
        }

        private static Autocancel AddAutocancelDataToTable(Move move, AppDbContext context)
        {
            var rawData = move.AutoCancel;

            var splitData = rawData.Split(',');

            var autocancel = new Autocancel
            {
                LastModified = DateTime.Now,
                Character = context.Characters.Single(c => c.Id == move.OwnerId),
                CharacterId = context.Characters.Single(c => c.Id == move.OwnerId).Id,
                Move = move,
                MoveId = move.Id
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

        private static BaseDamage AddBaseDamageDataToTable(Move move, AppDbContext context)
        {
            var baseDamage = SetBaseHitboxData<BaseDamage>(move.BaseDamage, '/', move, context);
            return baseDamage;
        }

        private static BaseKnockbackSetKnockback AddBaseKnockbackSetKnockback_DataTo_BaseKnockbackSetKnockback_Table(
            Move move, AppDbContext context)
        {
            var kbk = SetBaseHitboxData<BaseKnockbackSetKnockback>(move.BaseKnockBackSetKnockback, '/', move, context);

            return kbk;
        }

        private static Hitbox AddHitboxDataToHitboxTable(Move move, AppDbContext context)
        {
            var hitbox = SetBaseHitboxData<Hitbox>(move.HitboxActive, ',', move, context);
            return hitbox;
        }
    }

}
