using KuroganeHammer.Data.Core.Model.Characters;
using KuroganeHammer.Data.Core.Model.Stats;
using System.IO;

namespace KuroganeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            CaptainFalcon c = new CaptainFalcon();
            string json = c.AsJson<CaptainFalcon>(StatTypes.Special);
            //File.WriteAllText(@"E:\char\bowser.json", json);
        }
    }
}
