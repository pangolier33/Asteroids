using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Creatures.Enemy
{
    public class UfoMovement : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private SpaceShipMovement _spaceShip;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _spaceShip = FindFirstObjectByType<SpaceShipMovement>();

            StopAgentRotation();
        }

        private void FixedUpdate()
        {
            if (_navMeshAgent.enabled && _spaceShip)
                _navMeshAgent.SetDestination(_spaceShip.transform.position);
        }

        private void StopAgentRotation()
        {
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.updateUpAxis = false;
        }
    }
}
