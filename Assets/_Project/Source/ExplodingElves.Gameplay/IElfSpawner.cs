using UnityEngine;

namespace ExplodingElves.Gameplay
{
    public interface IElfSpawner
    {
        public delegate void ElfSpawnSignature();

        public event ElfSpawnSignature OnElfSpawned;

        int ActiveElvesCount { get; }

        float SpawnRate { get; set; }

        void SpawnElf(Vector3 position);
    }
}
