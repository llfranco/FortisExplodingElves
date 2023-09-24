using UnityEngine;

namespace ExplodingElves.Gameplay
{
    public interface IElfSpawner
    {
        public delegate void ElfSpawnSignature(IElf elf);

        public event ElfSpawnSignature OnElfSpawned;

        int ActiveElvesCount { get; }

        float SpawnRate { get; set; }

        void RandomlySpawnElf();

        void SpawnElf(Vector3 position);
    }
}
