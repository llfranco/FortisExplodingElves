using System.Collections.Generic;
using UnityEngine;

namespace ExplodingElves.Gameplay
{
    public sealed class ElfCollisionHandlingSystem : MonoBehaviour
    {
        private void Start()
        {
            IReadOnlyList<IElfSpawner> spawners = ServiceLocator.GetService<IElfSpawnerService>().GetSpawners();

            foreach (IElfSpawner spawner in spawners)
            {
                spawner.OnElfSpawned += HandleElfSpawned;
            }
        }

        private void HandleElfSpawned(IElf elf)
        {
            elf.OnElfCollisionEntered += HandleElfCollisionEntered;
        }

        private void HandleElfCollisionEntered(IElf self, IElf other)
        {
            if (self.Team.MatchesTeam(other.Team))
            {
                return;
            }

            self.OnElfCollisionEntered -= HandleElfCollisionEntered;
            Destroy(((Elf)self).gameObject);
        }
    }
}
