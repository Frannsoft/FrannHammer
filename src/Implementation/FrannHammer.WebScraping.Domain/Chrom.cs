using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.WebScraping.Domain
{
    public class Chrom : WebCharacter
    {
        public Chrom()
            : base("Chrom")
        {
            CssKey = "chrom";
        }
    }
}
