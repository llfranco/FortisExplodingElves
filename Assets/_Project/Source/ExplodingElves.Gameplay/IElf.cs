namespace ExplodingElves.Gameplay
{
    public interface IElf
    {
        public delegate void ElfCollisionSignature(IElf self, IElf other);

        public event ElfCollisionSignature OnElfCollisionEntered;

        TeamDefinition Team { get; }
    }
}
