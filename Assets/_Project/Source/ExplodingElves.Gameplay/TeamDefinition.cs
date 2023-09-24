using UnityEngine;

namespace ExplodingElves.Gameplay
{
    [CreateAssetMenu(menuName = "Exploding Elves/Create " + nameof(TeamDefinition), fileName = nameof(TeamDefinition))]
    public class TeamDefinition : ScriptableObject
    {
        [SerializeField]
        private int _id;

        [SerializeField]
        private string _displayName;

        [SerializeField]
        private Color _accentColor;

        public string DisplayName => _displayName;

        public Color AccentColor => _accentColor;

        public bool MatchesTeam(TeamDefinition otherTeam)
        {
            return _id == otherTeam._id;
        }
    }
}
