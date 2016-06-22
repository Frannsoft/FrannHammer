using System.Configuration;
using FrannHammer.Api.Models;

namespace KurograneHammer.TransferDBTool.Seeding
{
    internal class AppDbContext : ApplicationDbContext
    {
        public AppDbContext()
            : base(ConfigurationManager.AppSettings["connection"])
        { }
    }
}
