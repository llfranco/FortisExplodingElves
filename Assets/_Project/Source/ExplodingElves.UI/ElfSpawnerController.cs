using ExplodingElves.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ExplodingElves.UI
{
    public sealed class ElfSpawnerController : MonoBehaviour
    {
        private const string TargetTeamTextFormat = "TargetTeam: {0}";
        private const string ActiveElvesTextFormat = "Active Elves: {0}";
        private const string SpawnRateTextFormat = "Spawn Rate: {0:#.##}";

        [SerializeField]
        private TeamDefinition _targetTeam;

        [SerializeField]
        private TMP_Text _targetTeamText;

        [SerializeField]
        private TMP_Text _activeElvesText;

        [SerializeField]
        private TMP_Text _spawnRateText;

        [SerializeField]
        private Slider _spawnRateSlider;

        private IElfSpawner _targetElfSpawner;

        private void Start()
        {
            _targetElfSpawner = ServiceLocator.GetService<IElfSpawnerService>().GetTeamSpawner(_targetTeam);
            _targetElfSpawner.OnElfSpawned += HandleElfSpawnedOrDeSpawned;
            _targetElfSpawner.OnElfDeSpawned += HandleElfSpawnedOrDeSpawned;
            _targetTeamText.SetText(string.Format(TargetTeamTextFormat, _targetTeam.DisplayName));
            _activeElvesText.SetText(string.Format(ActiveElvesTextFormat, _targetElfSpawner.ActiveElvesCount));
            _spawnRateText.SetText(string.Format(SpawnRateTextFormat, _targetElfSpawner.SpawnRate));
            _spawnRateSlider.SetValueWithoutNotify(_targetElfSpawner.SpawnRate);
            _spawnRateSlider.onValueChanged.AddListener(HandleSpawnRateSliderValueChanged);
        }

        private void HandleElfSpawnedOrDeSpawned(IElf elf)
        {
            _activeElvesText.SetText(string.Format(ActiveElvesTextFormat, _targetElfSpawner.ActiveElvesCount));
        }

        private void HandleSpawnRateSliderValueChanged(float value)
        {
            _targetElfSpawner.SpawnRate = value;
            _spawnRateText.SetText(string.Format(SpawnRateTextFormat, value));
        }
    }
}
