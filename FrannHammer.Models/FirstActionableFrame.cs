namespace FrannHammer.Models
{
    public class FirstActionableFrame : BaseMeta, IMoveIdEntity
    {
        public string Frame { get; set; }
    }

    public class FirstActionableFrameDto
    {
        public string Frame { get; set; }
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int MoveId { get; set; }
        public string MoveName { get; set; }
        public string Notes { get; set; }
        public string RawValue { get; set; }
    }
}
