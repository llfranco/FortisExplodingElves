using System.Collections.Generic;
using UnityEngine;

namespace ExplodingElves.Gameplay
{
    public sealed class ElfCollisionHandlingSystem : MonoBehaviour
    {
        private readonly HashSet<(IElf, IElf)> _queuedCollisions = new();

        private IElfSpawnerService _spawnerService;

        private void Start()
        {
            _spawnerService = ServiceLocator.GetService<IElfSpawnerService>();

            foreach (IElfSpawner spawner in _spawnerService.GetSpawners())
            {
                spawner.OnElfSpawned += HandleElfSpawned;
            }
        }

        private void LateUpdate()
        {
            if (_queuedCollisions.Count > 0)
            {
                _queuedCollisions.Clear();
            }
        }

        private void HandleElfSpawned(IElf elf)
        {
            elf.OnElfCollisionEntered += HandleElfCollisionEntered;
        }

        private void HandleElfCollisionEntered(IElf self, IElf other)
        {
            if (!self.Team.MatchesTeam(other.Team))
            {
                self.OnElfCollisionEntered -= HandleElfCollisionEntered;
                _spawnerService.GetTeamSpawner(self.Team).QueueDeSpawn(self);

                return;
            }

            (IElf other, IElf self) collision = (other, self);

            if (_queuedCollisions.Contains(collision))
            {
                return;
            }

            _queuedCollisions.Add((self, other));
            _spawnerService.GetTeamSpawner(self.Team).QueueSpawn();
        }
    }
}
