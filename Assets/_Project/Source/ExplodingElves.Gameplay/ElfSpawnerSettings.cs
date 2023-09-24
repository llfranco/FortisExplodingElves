using UnityEngine;

namespace ExplodingElves.Gameplay
{
    [CreateAssetMenu(menuName = "Exploding Elves/Create " + nameof(ElfSpawnerSettings), fileName = nameof(ElfSpawnerSettings))]
    public sealed class ElfSpawnerSettings : ScriptableObject
    {
        [SerializeField]
        private float _defaultSpawnRate = 5f;

        [SerializeField]
        private Vector3 _collisionDetectionExtents;

        [SerializeField]
        private LayerMask _collisionDetectionMask;

        [SerializeField]
        private Elf _prefab;

        public float DefaultSpawnRate => _defaultSpawnRate;

        public Vector3 CollisionDetectionExtents => _collisionDetectionExtents;

        public LayerMask CollisionDetectionMask => _collisionDetectionMask;

        public Elf Prefab => _prefab;
    }
}
