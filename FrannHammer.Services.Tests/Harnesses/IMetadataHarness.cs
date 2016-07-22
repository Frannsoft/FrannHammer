namespace FrannHammer.Services.Tests.Harnesses
{
    public interface IMetadataHarness
    {
        string Fields { get; }
        void SingleIsValidMeta(int id, IApplicationDbContext context);
        void SingleIsValidMetaWithFields(int id, IApplicationDbContext context, string fields);
        void CollectionIsValidWithFields(IApplicationDbContext context, string fields = "");
        void CollectionIsValid(IApplicationDbContext context);
    }
}
