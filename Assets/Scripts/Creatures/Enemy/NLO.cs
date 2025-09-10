using UnityEngine;
using UnityEngine.AI;

public class NLO : MonoBehaviour
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
        if (_navMeshAgent.enabled)
            _navMeshAgent.SetDestination(_spaceShip.transform.position);
    }

    private void StopAgentRotation()
    {
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }

}
