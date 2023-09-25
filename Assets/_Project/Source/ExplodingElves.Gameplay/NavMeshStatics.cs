using UnityEngine;
using UnityEngine.AI;

namespace ExplodingElves.Gameplay
{
    public static class NavMeshStatics
    {
        public static Vector3 RandomNavMeshPositionWithinRadius(Vector3 sourcePosition, NavMeshAgent agent, float maxRadius)
        {
            Vector3 randomNearbyPosition = sourcePosition + Random.insideUnitSphere * maxRadius;

            return NavMesh.SamplePosition(randomNearbyPosition, out NavMeshHit hit, agent.radius * 4f, 1) ? hit.position : sourcePosition;
        }
    }
}
