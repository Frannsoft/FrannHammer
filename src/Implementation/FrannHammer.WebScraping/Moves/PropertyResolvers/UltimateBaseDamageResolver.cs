using FrannHammer.Domain.Contracts;
using HtmlAgilityPack;
using System.Linq;

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
            var normalBaseDamageNode = node.SelectSingleNode("./div[@class = 'tooltip']");
            if (normalBaseDamageNode != null)
            {
                string normalBaseDamage = normalBaseDamageNode.FirstChild.InnerText;
                if (normalBaseDamageNode.FirstChild.NextSibling != null && !normalBaseDamageNode.FirstChild.NextSibling.InnerText.Contains("1v1"))
                {
                    var allSpans = normalBaseDamageNode.SelectNodes("./span");
                    var numberSpans = allSpans.Where(sp =>
                    {
                        return int.TryParse(sp.InnerText, out int res);
                    });
                    foreach (var numberSpan in numberSpans.Skip(1))
                    {
                        if (normalBaseDamage.EndsWith("/"))
                        {
                            normalBaseDamage += numberSpan.InnerText;
                        }
                        else
                        {
                            normalBaseDamage += "/" + numberSpan.InnerText;
                        }
                    }
                    //handling ground/air only moves which have a slightly different dom (but only for those two values)
                    //normalBaseDamage += normalBaseDamageNode.FirstChild.NextSibling.InnerText;

                    //if (normalBaseDamageNode.FirstChild.NextSibling.NextSibling != null)
                    //{
                    //    var groundAirOnlyIndicatorNode = normalBaseDamageNode.FirstChild.NextSibling.NextSibling;
                    //    if (groundAirOnlyIndicatorNode != null && groundAirOnlyIndicatorNode.InnerText == "/")
                    //    {
                    //        normalBaseDamage += groundAirOnlyIndicatorNode.InnerText;
                    //        normalBaseDamage += normalBaseDamageNode.FirstChild.NextSibling.NextSibling.NextSibling.InnerText;
                    //    }
                    //}
                }
                string oneVoneBaseDamage = normalBaseDamageNode.LastChild.InnerText;
                basedmg = normalBaseDamage + "|" + oneVoneBaseDamage;
            }
            else
            {
                basedmg = node.InnerText;
            }

            return basedmg;
        }
    }

    public class Smash4HitboxResolver
    {
        public string Resolve(HtmlNode node) => node.InnerText;
    }

    public class UltimateHitboxResolver
    {
        public string Resolve(HtmlNode node)
        {
            string data = string.Empty;
            var allContentInHitboxNode = node.SelectSingleNode("./div[@class = 'tooltip']");
            if (allContentInHitboxNode != null)
            {
                string hitboxData = allContentInHitboxNode.FirstChild.InnerText;

                string remainingData = allContentInHitboxNode.LastChild.InnerText;
                data = hitboxData + "|" + remainingData;
            }
            else
            {
                data = node.InnerText;
            }

            return data;
        }
    }

    public class CommonAutocancelParameterResolver
    {
        public string Resolve(HtmlNode node)
        {
            string autoCancel = node.InnerText.Replace("&gt;", ">").Replace("&lt;", "<");
            return autoCancel;
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
                        move.HitboxActive = new UltimateHitboxResolver().Resolve(nodes[1]);
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
