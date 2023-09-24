using System.Collections.Generic;
using UnityEngine;

namespace ExplodingElves.Gameplay
{
    public sealed class ElfSpawnerSystem : MonoBehaviour, IElfSpawnerService
    {
        private Dictionary<TeamDefinition, ElfSpawner> _spawners = new();

        public IElfSpawner GetTeamSpawner(TeamDefinition team)
        {
            return _spawners[team];
        }

        private void Awake()
        {
            ElfSpawner[] spawners = FindObjectsOfType<ElfSpawner>(true);

            foreach (ElfSpawner spawner in spawners)
            {
                if (_spawners.ContainsKey(spawner.OwningTeam))
                {
                    Debug.LogWarning($"Found another spawner for team {spawner.OwningTeam}");
                }

                _spawners[spawner.OwningTeam] = spawner;
            }

            Debug.AssertFormat(_spawners.Count > 0, "{0} is empty", nameof(_spawners));
            ServiceLocator.SetService<IElfSpawnerService>(this);
        }
    }
}
