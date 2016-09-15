using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using FrannHammer.Models;
using NUnit.Framework;

namespace KuroganeHammer.TransferDBTool.Seeding
{
    internal class Seeder
    {
        //seed notations
        private const string NotationFloat = "FLOAT";
        private const string NotationFrames = "FRAMES";
        private const string NotationBool = "BOOLEAN";

        private readonly AppDbContext _context;

        internal Seeder(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public void SeedAll()
        {
            SeedThrowTypes();
            SeedThrowData();
            UpdateProblematicData();
            SeedMoveDetails();
            SeedNotations();
            SeedCharacterAttributesAndTypes();
            AddCachingTable();
            Console.WriteLine("Completed seeding and cleaning data.");
        }

        public void SeedMoveDetails()
        {
            SyncData<BaseKnockback>();
            SyncData<SetKnockback>();
            SyncData<BaseDamage>();
            SyncData<Hitbox>();
            SyncData<Angle>();
            SyncData<Autocancel>();
            SyncData<LandingLag>();
            SyncData<KnockbackGrowth>();
            SyncThrowData();
            Console.WriteLine("Completed seeding move details.");
        }

        private void SyncData<T>()
           where T : BaseMeta
        {
            if (!_context.Set<T>().Any())
            {
                var moves = _context.Moves.ToList().Where(m => !m.HitboxActive.Contains("No") &&
                !m.HitboxActive.Contains("Yes"));

                Console.WriteLine($"Adding {typeof(T).Name} data");

                var dataToAdd = moves.Select(move => TransferMethods.MapDataThenSync<T>(move, _context))
                    .Where(parsedData => parsedData != null)
                    .ToList();

                _context.Set<T>().AddRange(dataToAdd.Where(d => d != null)); //nulls exist for checks like bkb and wbkb
                _context.SaveChanges();
            }
        }

        //someone's always gotta be different
        private void SyncThrowData()
        {
            //correct the off by one error and add
            var moves = _context.Moves.ToList().Where(m => m.HitboxActive.Contains("No") ||
                    m.HitboxActive.Contains("Yes"));

            Console.WriteLine("Adding throw data...");

            foreach (var move in moves)
            {
                var tempBaseDamage = move.FirstActionableFrame;
                var tempAngle = move.BaseDamage;
                var tempBaseKnockback = move.Angle;
                var tempKnockbackGrowth = move.BaseKnockBackSetKnockback;

                move.BaseDamage = tempBaseDamage;
                move.Angle = tempAngle;
                move.KnockbackGrowth = tempKnockbackGrowth;
                move.BaseKnockBackSetKnockback = tempBaseKnockback;
                move.FirstActionableFrame = "-"; //do this until KH posts FAF data for throws
                move.HitboxActive = "-"; //clear out the invalid value here
                move.Type = MoveType.Throw; //update move type to throw

                var baseDmgData = TransferMethods.MapDataThenSync<BaseDamage>(move, _context);
                var angleData = TransferMethods.MapDataThenSync<Angle>(move, _context);
                var knockbackGrowthData = TransferMethods.MapDataThenSync<KnockbackGrowth>(move, _context);
                var baseKbk = TransferMethods.MapDataThenSync<BaseKnockback>(move, _context);
                var setKbk = TransferMethods.MapDataThenSync<SetKnockback>(move, _context);

                _context.Set<BaseDamage>().Add(baseDmgData);
                _context.Set<Angle>().Add(angleData);
                _context.Set<KnockbackGrowth>().Add(knockbackGrowthData);

                if (baseKbk != null)
                {
                    _context.Set<BaseKnockback>().Add(baseKbk);
                }
                if (setKbk != null)
                {
                    _context.Set<SetKnockback>().Add(setKbk);
                }
            }
            _context.SaveChanges();
            //can correct firstactiveframe in the future
        }

        public void SeedThrowTypes()
        {
            //seed throw types
            if (!_context.ThrowTypes.Any())
            {
                var forwardThrow = new ThrowType
                {
                    LastModified = DateTime.Now,
                    Name = "Forward"
                };

                var backThrow = new ThrowType
                {
                    LastModified = DateTime.Now,
                    Name = "Back"
                };

                var upThrow = new ThrowType
                {
                    LastModified = DateTime.Now,
                    Name = "Up"
                };

                var downThrow = new ThrowType
                {
                    LastModified = DateTime.Now,
                    Name = "Down"
                };

                _context.ThrowTypes.Add(forwardThrow);
                _context.ThrowTypes.Add(backThrow);
                _context.ThrowTypes.Add(upThrow);
                _context.ThrowTypes.Add(downThrow);
                _context.SaveChanges();
            }
        }

        public void SeedThrowData()
        {
            //seed throw data
            if (!_context.Throws.Any())
            {
                var allMoves = _context.Moves.ToList().Where(m => m.Name.Contains("throw")).ToList();

                foreach (var move in allMoves)
                {
                    var newThrow = new Throw
                    {
                        LastModified = DateTime.Now,
                        MoveId = move.Id,
                        Move = move
                    };

                    if (move.Name.Contains("Fthrow"))
                    {
                        newThrow.ThrowTypeId = _context.ThrowTypes.First(t => t.Name == "Forward").Id;
                    }
                    else if (move.Name.Contains("Bthrow"))
                    {
                        newThrow.ThrowTypeId = _context.ThrowTypes.First(t => t.Name == "Back").Id;
                    }
                    else if (move.Name.Contains("Uthrow"))
                    {
                        newThrow.ThrowTypeId = _context.ThrowTypes.First(t => t.Name == "Up").Id;
                    }
                    else if (move.Name.Contains("Dthrow"))
                    {
                        newThrow.ThrowTypeId = _context.ThrowTypes.First(t => t.Name == "Down").Id;
                    }
                    else //is not actual throw move.  Just a move that has the word throw in it (e.g., 'flamethrower' you dawg)
                    {
                        continue;
                    }

                    var hitboxActiveText = move.HitboxActive;

                    if (hitboxActiveText.Contains("No"))
                    {
                        newThrow.WeightDependent = false;
                    }
                    if (hitboxActiveText.Contains("Yes"))
                    {
                        newThrow.WeightDependent = true;
                    }

                    _context.Throws.Add(newThrow);
                    _context.SaveChanges();
                }
            }
        }

        public void SeedNotations()
        {
            if (!_context.Notations.Any())
            {
                var floatNotation = new Notation
                {
                    Name = NotationFloat,
                    LastModified = DateTime.Now
                };

                var framesNotation = new Notation
                {
                    Name = NotationFrames,
                    LastModified = DateTime.Now
                };

                var booleanNotation = new Notation
                {
                    Name = NotationBool,
                    LastModified = DateTime.Now
                };

                _context.Notations.Add(floatNotation);
                _context.Notations.Add(framesNotation);
                _context.Notations.Add(booleanNotation);
            }
            _context.SaveChanges();
        }

        public void SeedCharacterAttributesAndTypes()
        {
            if (_context.CharacterAttributes.Any() && !_context.CharacterAttributeTypes.Any())
            {
                var charAttributeNames = _context.CharacterAttributes.Select(c => c.Name).Distinct().ToList();//get names

                //add char attribute type ids
                foreach (var name in charAttributeNames)
                {
                    int notationId;

                    if (name.Equals("FAST FALL SPEED") ||
                        name.Equals("MAX AIR SPEED VALUE") ||
                        name.Equals("MAX FALL SPEED VALUE") ||
                        name.Equals("MAX FALL SPEED") ||
                        name.Equals("MAX WALK SPEED VALUE") ||
                        name.Equals("SPEED INCREASE") ||
                        name.Equals("VALUE") ||
                        name.Equals("WEIGHT VALUE"))
                    {
                        notationId = _context.Notations.First(n => n.Name.Equals(NotationFloat)).Id;
                    }
                    else if (name.Equals("INTANGIBILITY") ||
                             name.Equals("INTANGIBLE") ||
                             name.Equals("FAF"))
                    {
                        notationId = _context.Notations.First(n => n.Name.Equals(NotationFrames)).Id;
                    }
                    else
                    {
                        notationId = _context.Notations.First(n => n.Name.Equals(NotationBool)).Id;
                    }


                    var charAttributeType = new CharacterAttributeType
                    {
                        NotationId = notationId,
                        LastModified = DateTime.Now,
                        Name = name
                    };
                    _context.CharacterAttributeTypes.Add(charAttributeType);
                }
                _context.SaveChanges();

                //now set the characterattributes
                foreach (var characterAttribute in _context.CharacterAttributes.ToList())
                {
                    characterAttribute.CharacterAttributeTypeId = _context.CharacterAttributeTypes.Single(c => c.Name.Equals(characterAttribute.Name)).Id;
                    _context.CharacterAttributes.AddOrUpdate(characterAttribute);
                }
                _context.SaveChanges();
            }
        }

        public void UpdateProblematicData()
        {
            if (_context.Moves.Any())
            {
                //update throw in moves to proper movetype
                //UpdateThrowInMovesToProperMoveType(_context);

                //remove the following moves from the moves table.  
                //They are character attributes and are already stored in the character attributes table.  
                RemoveProblematicMovesFromTable();

                //remove mid-table column header data
                RemoveColumnHeaderDate();
            }
        }

        public void AddCachingTable()
        {
            //setup cachecow in db
            var sqlScript =
                File.ReadAllText(
                    @"E:\Development\C#projects\FrannHammer\packages\CacheCow.Server.EntityTagStore.SqlServer.1.0.0\scripts\script.sql");

            // Split by "GO" statements
            var statements = Regex.Split(
                    sqlScript,
                    @"^\s*GO\s*\d*\s*($|\-\-.*$)",
                    RegexOptions.Multiline |
                    RegexOptions.IgnorePatternWhitespace |
                    RegexOptions.IgnoreCase);

            // Remove empties, trim, and return
            var filteredStatements = statements
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim(' ', '\r', '\n'));

            foreach (var statement in filteredStatements)
            {
                _context.Database.ExecuteSqlCommand(statement);
            }
        }

        private void UpdateThrowInMovesToProperMoveType()
        {
            var allThrowMoves = _context.Moves.ToList().Where(m => m.Name.Contains("throw"));

            foreach (var move in allThrowMoves)
            {
                move.Type = MoveType.Throw;
            }
            _context.SaveChanges();
        }

        private void RemoveProblematicMovesFromTable()
        {
            var allProblemMoves = _context.Moves.ToList().Where(m => m.Name.Equals("Spotdodge") ||
                                                                       m.Name.Equals("Airdodge") ||
                                                                       m.Name.Equals("Forward Roll") ||
                                                                       m.Name.Equals("Back Roll"));
            foreach (var move in allProblemMoves)
            {
                _context.Moves.Remove(move);
            }

            _context.SaveChanges();
        }

        private void RemoveColumnHeaderDate()
        {
            var allColumnHeaderMoves = _context.Moves.ToList().Where(m => m.HitboxActive.Equals("Hitbox Active") ||
                                                                             m.HitboxActive.Equals("Weight Dependent?") ||
                                                                             m.HitboxActive.Equals("Weight Dependant?") ||
                                                                             m.HitboxActive.Equals("Intangibility"));
            foreach (var move in allColumnHeaderMoves)
            {
                _context.Moves.Remove(move);
            }

            _context.SaveChanges();
        }
    }
}
