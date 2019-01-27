using FrannHammer.Domain.Contracts;
using HtmlAgilityPack;

namespace FrannHammer.WebScraping.Moves
{
    public class Smash4BaseDamageResolver
    {
        public string GetRawValue(HtmlNode node) => node.InnerText;
    }

    public class UltimateBaseDamageResolver
    {
        public string GetRawValue(HtmlNode node)
        {
            string basedmg = string.Empty;
            // var baseDmgCell = node;//<div class="tooltip">11<span class="tooltiptext">1v1: 13.2</span></div>
            var normalBaseDamageNode = node.SelectSingleNode("./div[@class = 'tooltip']");
            if (normalBaseDamageNode != null)
            {
                string normalBaseDamage = normalBaseDamageNode.FirstChild.InnerText;

                string oneVoneBaseDamage = normalBaseDamageNode.LastChild.InnerText;
                basedmg = normalBaseDamage + oneVoneBaseDamage;
            }
            else
            {
                basedmg = node.InnerText;
            }

            return basedmg;
        }
    }

    //public class BaseDamageResolver
    //{
    //    private readonly Games _game;

    //    public BaseDamageResolver(Games game)
    //    {
    //        _game = game;
    //    }

    //    public string Resolve(HtmlNode node)
    //    {
    //        switch (_game)
    //        {
    //            case Games.Smash4:
    //                {
    //                    return new Smash4BaseDamageResolver().GetRawValue(node);
    //                }
    //            case Games.Ultimate:
    //                {
    //                    return new UltimateBaseDamageResolver().GetRawValue(node);
    //                }
    //            default:
    //                {
    //                    return new Smash4BaseDamageResolver().GetRawValue(node);
    //                }
    //        }
    //    }
    //}

    public class Smash4HitboxResolver
    {
        public string Resolve(HtmlNode node) => node.InnerText;
    }

    public class UltimateHitboxResolver
    {
        public string Resolve(HtmlNode node)
        {
            string hitbox = string.Empty;
            var allContentInHitboxNode = node.SelectSingleNode("./div[@class = 'tooltip']");
            if (allContentInHitboxNode != null)
            {
                string hitboxData = allContentInHitboxNode.FirstChild.InnerText;
                string advData = allContentInHitboxNode.LastChild.InnerText;
                hitbox = hitbox + advData;
            }
            else
            {
                hitbox = node.InnerText;
            }

            return hitbox;
        }
    }

    /// <summary>
    /// Covers getting hitbox and base damage data.
    /// </summary>
    public class CommonMoveParameterResolver
    {
        private readonly Games _game;

        public CommonMoveParameterResolver(Games game)
        {
            _game = game;
        }

        public IMove Resolve(HtmlNodeCollection nodes, IMove move)
        {
            switch (_game)
            {
                case Games.Smash4:
                    {
                        ResolveUsingSmash4(nodes, move);
                        break;
                    }
                case Games.Ultimate:
                    {
                        move.HitboxActive = new Smash4HitboxResolver().Resolve(nodes[1]);
                        move.BaseDamage = new UltimateBaseDamageResolver().GetRawValue(nodes[3]);
                        move.Game = Games.Ultimate;
                        break;
                    }
                default:
                    {
                        ResolveUsingSmash4(nodes, move);
                        break;
                    }
            }

            return move;
        }

        private IMove ResolveUsingSmash4(HtmlNodeCollection nodes, IMove move)
        {
            move.HitboxActive = new Smash4HitboxResolver().Resolve(nodes[1]);
            move.BaseDamage = new Smash4BaseDamageResolver().GetRawValue(nodes[3]);
            move.Game = Games.Smash4;
            return move;
        }
    }
}
