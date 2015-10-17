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
            c.SaveCharacterToDatabase();
            string json = c.AsJson<CaptainFalcon>(StatTypes.Aerial);
            //File.WriteAllText(@"E:\char\cfalconmovement.json", json);
        }
    }
}
