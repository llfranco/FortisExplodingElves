using UnityEngine;

namespace ExplodingElves.Gameplay
{
    public sealed class ElfCollisionHandlingSystem : MonoBehaviour
    {
        private IElfSpawnerService _spawnerService;

        private void Start()
        {
            _spawnerService = ServiceLocator.GetService<IElfSpawnerService>();

            foreach (IElfSpawner spawner in _spawnerService.GetSpawners())
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
