namespace FrannHammer.Services.Tests.Harnesses
{
    public interface IMoveDataHarness
    {
        string Fields { get; }
        void SingleIsValidMove(int id, IApplicationDbContext context);
        void SingleIsValidMoveWithFields(int id, IApplicationDbContext context, string fields);
        void CollectionIsValidWithFields(IApplicationDbContext context, string fields = "");
        void CollectionIsValid(IApplicationDbContext context);
    }
}
