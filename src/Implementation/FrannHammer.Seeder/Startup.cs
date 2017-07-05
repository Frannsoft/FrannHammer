using FrannHammer.Domain;

namespace FrannHammer.Seeder
{
    public static class Startup
    {
        public static void InitializeMapping()
        {
            BsonMapper.RegisterTypeWithAutoMap<MongoModel>();
            BsonMapper.RegisterClassMaps(typeof(Character), typeof(Movement), typeof(Move),
                typeof(CharacterAttributeRow), typeof(CharacterAttribute), typeof(UniqueData));
        }
    }
}