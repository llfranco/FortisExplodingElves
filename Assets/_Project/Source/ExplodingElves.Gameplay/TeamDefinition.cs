using UnityEngine;

namespace ExplodingElves.Gameplay
{
    [CreateAssetMenu(menuName = "Exploding Elves/Create TeamDefinition", fileName = "TeamDefinition")]
    public class TeamDefinition : ScriptableObject
    {
        [SerializeField]
        private int _id;

        [SerializeField]
        private Color _accentColor;

        public Color AccentColor => _accentColor;

        public bool MatchesTeam(TeamDefinition otherTeam)
        {
            return _id == otherTeam._id;
        }
    }
}
