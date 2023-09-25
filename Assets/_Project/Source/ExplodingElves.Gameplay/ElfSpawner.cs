using System.Collections.Generic;
using UnityEngine;

namespace ExplodingElves.Gameplay
{
    public sealed class ElfSpawner : MonoBehaviour, IElfSpawner
    {
        public event IElfSpawner.ElfSpawnSignature OnElfSpawned;

        private const float ColorAlpha = 0.6f;

        private static readonly int ColorPropertyId = Shader.PropertyToID("_Color");

        private readonly Collider[] _collisionDetectionBuffer = new Collider[1];
        private readonly Stack<Elf> _pooledElves = new();
        private readonly List<Elf> _activeElves = new();

        [SerializeField]
        private ElfSpawnerSettings _settings;

        [SerializeField]
        private TeamDefinition _owningTeam;

        [SerializeField]
        private MeshRenderer _renderer;

        [SerializeField]
        private Transform[] _spawnPoints;

        private uint _queuedSpawnsCount;

        public TeamDefinition OwningTeam => _owningTeam;

        public int ActiveElvesCount => _activeElves.Count;

        public float SpawnRate { get; set; }

        public void QueueSpawn()
        {
            _queuedSpawnsCount++;
        }

        public void QueueDeSpawn(IElf elf)
        {
            Elf typedElf = (Elf)elf;
            typedElf.Dispose();

            _activeElves.Remove(typedElf);
            _pooledElves.Push(typedElf);
        }

        private void OnValidate()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        private void Awake()
        {
            Debug.AssertFormat(_settings != null, "{0} is null", nameof(_settings));
            Debug.AssertFormat(_owningTeam != null, "{0} is null", nameof(_owningTeam));
            Debug.AssertFormat(_renderer != null, "{0} is null", nameof(_renderer));
            Debug.AssertFormat(_spawnPoints.Length > 0, "{0} is empty", nameof(_spawnPoints));

            SpawnRate = _settings.DefaultSpawnRate;

            MaterialPropertyBlock propertyBlock = new();
            Color color = new(_owningTeam.AccentColor.r, _owningTeam.AccentColor.g, _owningTeam.AccentColor.b, ColorAlpha);
            propertyBlock.SetColor(ColorPropertyId, color);

            _renderer.SetPropertyBlock(propertyBlock);
        }

        private void Start()
        {
            Invoke(nameof(InvokeQueueSpawn), SpawnRate);
        }

        private void Update()
        {
            if (_queuedSpawnsCount == 0 || !TryFindSpawnPoint(out Transform spawnPoint))
            {
                return;
            }

            SpawnElf(spawnPoint.position);
            _queuedSpawnsCount--;
        }

        private void InvokeQueueSpawn()
        {
            QueueSpawn();
            Invoke(nameof(InvokeQueueSpawn), SpawnRate);
        }

        public void SpawnElf(Vector3 position)
        {
            Elf elf = _pooledElves.Count > 0 ? _pooledElves.Pop() : Instantiate(_settings.Prefab);
            elf.Setup(position, Quaternion.identity, _owningTeam);
            _activeElves.Add(elf);

            OnElfSpawned?.Invoke(elf);;
        }

        private bool TryFindSpawnPoint(out Transform foundSpawnPoint)
        {
            foreach (Transform spawnPoint in _spawnPoints)
            {
                if (Physics.OverlapBoxNonAlloc(spawnPoint.position, _settings.CollisionDetectionExtents, _collisionDetectionBuffer, Quaternion.identity, _settings.CollisionDetectionMask) > 0)
                {
                    continue;
                }

                foundSpawnPoint = spawnPoint;

                return true;
            }

            foundSpawnPoint = default;

            return false;
        }
    }
}
