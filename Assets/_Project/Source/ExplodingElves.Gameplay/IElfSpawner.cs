using UnityEngine;

namespace ExplodingElves.Gameplay
{
    public interface IElfSpawner
    {
        void SpawnElf(Vector3 position, TeamDefinition team);
    }
}
