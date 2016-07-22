using System.Configuration;
using FrannHammer.Api.Models;
using FrannHammer.Services;

namespace KurograneHammer.TransferDBTool.Seeding
{
    internal class AppDbContext : ApplicationDbContext
    {
        public AppDbContext()
            : base(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)
        { }
    }
}
