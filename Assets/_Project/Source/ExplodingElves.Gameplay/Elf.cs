using UnityEngine;
using UnityEngine.AI;

namespace ExplodingElves.Gameplay
{
    public sealed class Elf : MonoBehaviour, IElf
    {
        public event IElf.ElfCollisionSignature OnElfCollisionEntered;

        private const float DistanceThreshold = 0.1f;

        private static readonly int ColorPropertyId = Shader.PropertyToID("_Color");

        [SerializeField]
        private ElfDefinition _definition;

        [SerializeField]
        private MeshRenderer _renderer;

        [SerializeField]
        private NavMeshAgent _navMeshAgent;

        private Transform _transform;

        public TeamDefinition Team { get; private set; }

        public void Setup(TeamDefinition team)
        {
            Team = team;

            MaterialPropertyBlock propertyBlock = new();
            propertyBlock.SetColor(ColorPropertyId, team.AccentColor);

            _renderer.SetPropertyBlock(propertyBlock);
        }

        private void OnValidate()
        {
            _renderer = GetComponent<MeshRenderer>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(_navMeshAgent.destination, 0.5f);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Elf otherElf))
            {
                OnElfCollisionEntered?.Invoke(this, otherElf);
            }
        }

        private void Awake()
        {
            Debug.AssertFormat(_renderer != null, "{0} is null", nameof(_renderer));
            Debug.AssertFormat(_navMeshAgent != null, "{0} is null", nameof(_navMeshAgent));

            _transform = transform;
        }

        private void Update()
        {
            if (_navMeshAgent.remainingDistance > DistanceThreshold)
            {
                return;
            }
            Vector3 nextDestination = NavMeshStatics.RandomNavMeshPositionWithinRadius(_transform.position, _definition.DestinationSearchingRadius);

            _navMeshAgent.SetDestination(nextDestination);
        }
    }
}
