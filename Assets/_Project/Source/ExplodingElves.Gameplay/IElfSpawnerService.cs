using System.Collections.Generic;

namespace ExplodingElves.Gameplay
{
    public interface IElfSpawnerService
    {
        IElfSpawner GetTeamSpawner(TeamDefinition team);

        IReadOnlyList<IElfSpawner> GetSpawners();
    }
}
