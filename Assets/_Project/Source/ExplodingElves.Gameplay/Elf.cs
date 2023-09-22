using UnityEngine;

namespace ExplodingElves.Gameplay
{
    public sealed class Elf : MonoBehaviour
    {
        private static readonly int ColorPropertyId = Shader.PropertyToID("_Color");

        [SerializeField]
        private MeshRenderer _renderer;

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
        }

        private void Awake()
        {
            Debug.AssertFormat(_renderer != null, "{0} is null", nameof(_renderer));
        }
    }
}
