using System.Configuration;
using KuroganeHammer.Data.Api.Models;

namespace KurograneTransferDBTool.Seeding
{
    internal class AppDbContext : ApplicationDbContext
    {
        public AppDbContext()
            : base(ConfigurationManager.AppSettings["connection"])
        { }
    }
}
