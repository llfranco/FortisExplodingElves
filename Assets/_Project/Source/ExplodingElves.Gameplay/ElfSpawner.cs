using System.Collections.Generic;
using UnityEngine;

namespace ExplodingElves.Gameplay
{
    public sealed class ElfSpawner : MonoBehaviour, IElfSpawner
    {
        public event IElfSpawner.ElfSpawnSignature OnElfSpawned;

        private const float ColorAlpha = 0.6f;

        private static readonly int ColorPropertyId = Shader.PropertyToID("_Color");

        private readonly List<Elf> _activeElves = new();

        [SerializeField]
        private ElfSpawnerSettings _settings;

        [SerializeField]
        private TeamDefinition _owningTeam;

        [SerializeField]
        private MeshRenderer _renderer;

        [SerializeField]
        private Transform[] _spawnPoints;

        public TeamDefinition OwningTeam => _owningTeam;

        public int ActiveElvesCount => _activeElves.Count;

        public float SpawnRate { get; set; }

        public void SpawnElf(Vector3 position)
        {
            Elf elf = Instantiate(_settings.Prefab, position, Quaternion.identity);
            elf.Setup(_owningTeam);

            _activeElves.Add(elf);

            OnElfSpawned?.Invoke(elf);;
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
            Invoke(nameof(RandomlySpawnElf), SpawnRate);
        }

        private void RandomlySpawnElf()
        {
            SpawnElf(_spawnPoints[Random.Range(0, _spawnPoints.Length)].position);
            Invoke(nameof(RandomlySpawnElf), SpawnRate);
        }
    }
}
