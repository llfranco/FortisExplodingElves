using UnityEngine;

namespace ExplodingElves.Gameplay
{
    public sealed class ElfSpawner : MonoBehaviour, IElfSpawner
    {
        private const float ColorAlpha = 0.6f;

        private static readonly int ColorPropertyId = Shader.PropertyToID("_Color");

        [SerializeField]
        private ElfSpawnerSettings _settings;

        [SerializeField]
        private TeamDefinition _owningTeam;

        [SerializeField]
        private MeshRenderer _renderer;

        public void SpawnElf(Vector3 position, TeamDefinition team)
        {
            Elf elf = Instantiate(_settings.Prefab, position, Quaternion.identity);
            elf.Setup(team);
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

            MaterialPropertyBlock propertyBlock = new();
            Color color = new(_owningTeam.AccentColor.r, _owningTeam.AccentColor.g, _owningTeam.AccentColor.b, ColorAlpha);
            propertyBlock.SetColor(ColorPropertyId, color);

            _renderer.SetPropertyBlock(propertyBlock);
        }

        private void Start()
        {
            InvokeRepeating(nameof(RandomlySpawnElf), _settings.DefaultSpawnRate, _settings.DefaultSpawnRate);
        }

        private void RandomlySpawnElf()
        {
            SpawnElf(transform.position, _owningTeam);
        }
    }
}
