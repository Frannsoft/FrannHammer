using System;
using System.Linq;
using FrannHammer.Core.Models;

namespace KurograneHammer.TransferDBTool.Seeding
{
    internal class Seeder
    {
        public void Seed()
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

   

    
}
