using System;
using System.Linq;
using FrannHammer.Models;
using NUnit.Framework;

namespace KurograneHammer.TransferDBTool.Seeding
{
    [TestFixture]
    public class Seeding
    {
        [Test]
        [Explicit("Parses and splits move data into specific move tables")]
        public void SeedSpecificMoveData()
        {
            var seeder = new Seeder();
            seeder.Seed();

            //if completes assume success..
        }
    }

    internal class Seeder
    {
        public void Seed()
        {
            using (AppDbContext context = new AppDbContext())
            {
                SyncData<BaseDamage>(context);
                SyncData<Hitbox>(context);
                //SyncData<BaseKnockbackSetKnockback>(context);
                SyncData<Angle>(context);
                SyncData<Autocancel>(context);
                SyncData<LandingLag>(context);
                SyncData<KnockbackGrowth>(context);
                SyncThrowData(context);
            }
        }

        private static void SyncData<T>(AppDbContext context)
           where T : BaseMeta
        {
            if (!context.Set<T>().Any())
            {
                var moves = context.Moves.ToList();
                Console.WriteLine($"Adding {typeof(T).Name} data");

                foreach (var move in moves)
                {
                    if (!move.HitboxActive.Contains("No") &&
                        !move.HitboxActive.Contains("Yes"))
                    {
                        var metaData = TransferMethods.MapDataThenSync<T>(move, context);
                        context.Set<T>().Add(metaData);
                    }
                }
                context.SaveChanges();
            }
        }

        //someones always gotta be different
        private static void SyncThrowData(AppDbContext context)
        {
            //correct the offby one error and add
            var moves = context.Moves.ToList();

            foreach (var move in moves)
            {
                if (move.HitboxActive.Contains("No") ||
                    move.HitboxActive.Contains("Yes"))
                {
                    var tempBaseDamage = move.FirstActionableFrame;
                    var tempAngle = move.BaseDamage;
                    //var tempBaseKnockback = move.Angle; TODO: not yet parseable
                    var tempKnockbackGrowth = move.BaseKnockBackSetKnockback;

                    move.BaseDamage = tempBaseDamage;
                    move.Angle = tempAngle;
                    move.KnockbackGrowth = tempKnockbackGrowth;

                    var baseDmgData = TransferMethods.MapDataThenSync<BaseDamage>(move, context);
                    var angleData = TransferMethods.MapDataThenSync<Angle>(move, context);
                    var knockbackGrowthData = TransferMethods.MapDataThenSync<KnockbackGrowth>(move, context);
                    Console.WriteLine($"Adding throw data for moveId: {move.Id}");

                    context.Set<BaseDamage>().Add(baseDmgData);
                    context.Set<Angle>().Add(angleData);
                    context.Set<KnockbackGrowth>().Add(knockbackGrowthData);
                }
            }
            context.SaveChanges();
            //can correct firstactiveframe in the future
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




}
