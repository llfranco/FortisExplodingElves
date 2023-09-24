﻿using ExplodingElves.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ExplodingElves.UI
{
    public sealed class ElfSpawnerController : MonoBehaviour
    {
        private const string TargetTeamTextFormat = "TargetTeam: {0}";
        private const string ActiveElvesTextFormat = "Active Elves: {0}";

        [SerializeField]
        private TeamDefinition _targetTeam;

        [SerializeField]
        private TMP_Text _targetTeamText;

        [SerializeField]
        private TMP_Text _activeElvesText;

        [SerializeField]
        private Slider _spawnRateSlider;

        private IElfSpawner _targetElfSpawner;

        private void Start()
        {
            _targetElfSpawner = ServiceLocator.GetService<IElfSpawnerService>().GetTeamSpawner(_targetTeam);
            _targetElfSpawner.OnElfSpawned += HandleElfSpawned;
            _targetTeamText.SetText(string.Format(TargetTeamTextFormat, _targetTeam.DisplayName));
            _activeElvesText.SetText(string.Format(ActiveElvesTextFormat, _targetElfSpawner.ActiveElvesCount));
            _spawnRateSlider.SetValueWithoutNotify(_targetElfSpawner.SpawnRate);
            _spawnRateSlider.onValueChanged.AddListener(HandleSpawnRateSliderValueChanged);
        }

        private void HandleElfSpawned()
        {
            _activeElvesText.SetText(string.Format(ActiveElvesTextFormat, _targetElfSpawner.ActiveElvesCount));
        }

        private void HandleSpawnRateSliderValueChanged(float value)
        {
            _targetElfSpawner.SpawnRate = value;
        }
    }
}
