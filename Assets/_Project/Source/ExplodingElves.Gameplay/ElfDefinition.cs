using UnityEngine;

namespace ExplodingElves.Gameplay
{
    [CreateAssetMenu(menuName = "Exploding Elves/Create " + nameof(ElfDefinition), fileName = nameof(ElfDefinition))]
    public sealed class ElfDefinition : ScriptableObject
    {
        [SerializeField]
        private float _destinationSearchingRadius = 10f;

        public float DestinationSearchingRadius => _destinationSearchingRadius;
    }
}
