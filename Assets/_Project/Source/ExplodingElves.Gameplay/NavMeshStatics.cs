using UnityEngine;
using UnityEngine.AI;

namespace ExplodingElves.Gameplay
{
    public static class NavMeshStatics
    {
        public static Vector3 RandomNavMeshPositionWithinRadius(Vector3 sourcePosition, float radius)
        {
            Vector3 randomNearbyPosition = sourcePosition + Random.insideUnitSphere * radius;

            return NavMesh.SamplePosition(randomNearbyPosition, out NavMeshHit hit, radius, 1) ? hit.position : sourcePosition;
        }
    }
}
