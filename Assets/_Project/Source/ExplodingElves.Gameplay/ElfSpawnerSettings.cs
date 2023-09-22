using UnityEngine;

namespace ExplodingElves.Gameplay
{
    [CreateAssetMenu(menuName = "Exploding Elves/Create " + nameof(ElfSpawnerSettings), fileName = nameof(ElfSpawnerSettings))]
    public sealed class ElfSpawnerSettings : ScriptableObject
    {
        [SerializeField]
        private float _defaultSpawnRate = 5f;

        [SerializeField]
        private Elf _prefab;

        public float DefaultSpawnRate => _defaultSpawnRate;

        public Elf Prefab => _prefab;
    }
}
