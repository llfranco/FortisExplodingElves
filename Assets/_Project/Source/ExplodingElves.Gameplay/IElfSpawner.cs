﻿namespace ExplodingElves.Gameplay
{
    public interface IElfSpawner
    {
        public delegate void ElfSpawnSignature(IElf elf);

        public event ElfSpawnSignature OnElfSpawned;

        public event ElfSpawnSignature OnElfDeSpawned;

        int ActiveElvesCount { get; }

        float SpawnRate { get; set; }

        void QueueSpawn();

        void QueueDeSpawn(IElf elf);
    }
}
