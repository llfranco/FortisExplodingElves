using UnityEngine;
using UnityEngine.AI;

namespace ExplodingElves.Gameplay
{
    public sealed class Elf : MonoBehaviour
    {
        private static readonly int ColorPropertyId = Shader.PropertyToID("_Color");

        [SerializeField]
        private ElfDefinition _definition;

        [SerializeField]
        private MeshRenderer _renderer;

        [SerializeField]
        private NavMeshAgent _navMeshAgent;

        private Transform _transform;
        private TeamDefinition _team;

        public void Setup(TeamDefinition team)
        {
            _team = team;

            MaterialPropertyBlock propertyBlock = new();
            propertyBlock.SetColor(ColorPropertyId, team.AccentColor);

            _renderer.SetPropertyBlock(propertyBlock);
        }

        private void OnValidate()
        {
            _renderer = GetComponent<MeshRenderer>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Awake()
        {
            Debug.AssertFormat(_renderer != null, "{0} is null", nameof(_renderer));
            Debug.AssertFormat(_navMeshAgent != null, "{0} is null", nameof(_navMeshAgent));

            _transform = transform;
        }

        private void Update()
        {
            if (_navMeshAgent.pathStatus != NavMeshPathStatus.PathComplete)
            {
                return;
            }

            Vector3 nextDestination = NavMeshStatics.RandomNavMeshPositionWithinRadius(_transform.position, _definition.DestinationSearchingRadius);

            _navMeshAgent.SetDestination(nextDestination);
        }
    }
}
