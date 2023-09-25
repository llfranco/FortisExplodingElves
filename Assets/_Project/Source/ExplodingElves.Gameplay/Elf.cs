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

        private bool _hasAwakened;
        private GameObject _gameObject;
        private Transform _transform;

        public bool IsActive { get; private set; }

        public TeamDefinition Team { get; private set; }

        public void Setup(Vector3 position, Quaternion rotation, TeamDefinition team)
        {
            if (!_hasAwakened)
            {
                Awake();
            }

            _transform.position = position;
            _transform.rotation = rotation;

            IsActive = true;
            Team = team;

            MaterialPropertyBlock propertyBlock = new();
            propertyBlock.SetColor(ColorPropertyId, team.AccentColor);

            _renderer.SetPropertyBlock(propertyBlock);
            _gameObject.SetActive(true);
        }

        public void Dispose()
        {
            IsActive = false;
            Team = default;

            _gameObject.SetActive(false);
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
            if (_hasAwakened)
            {
                return;
            }

            Debug.AssertFormat(_renderer != null, "{0} is null", nameof(_renderer));
            Debug.AssertFormat(_navMeshAgent != null, "{0} is null", nameof(_navMeshAgent));

            _gameObject = gameObject;
            _transform = transform;
            _hasAwakened = true;
        }

        private void Update()
        {
            if (!IsActive || _navMeshAgent.remainingDistance > DistanceThreshold)
            {
                return;
            }

            Vector3 nextDestination = NavMeshStatics.RandomNavMeshPositionWithinRadius(_transform.position, _navMeshAgent, _definition.DestinationSearchingRadius);

            _navMeshAgent.SetDestination(nextDestination);
        }
    }
}
