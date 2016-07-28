using System.Configuration;
using FrannHammer.Services;

namespace KuroganeHammer.TransferDBTool.Seeding
{
    internal class AppDbContext : ApplicationDbContext
    {
        public AppDbContext()
            : base(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)
        { }
    }
}
