using UnityEngine;

namespace ExplodingElves.Gameplay
{
    [CreateAssetMenu(menuName = "Exploding Elves/Create ElfSpawnerSettings", fileName = "ElfSpawnerSettings")]
    public sealed class ElfSpawnerSettings : ScriptableObject
    {
        [SerializeField]
        private float _defaultSpawnRate;

        [SerializeField]
        private Elf _prefab;

        public float DefaultSpawnRate => _defaultSpawnRate;

        public Elf Prefab => _prefab;
    }
}
