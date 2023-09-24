namespace ExplodingElves.Gameplay
{
    public interface IElfSpawnerService
    {
        IElfSpawner GetTeamSpawner(TeamDefinition team);
    }
}
