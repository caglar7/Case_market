

using UnityEngine;
using UnityEngine.AI;

public class BaseAgentMover : BaseMovement 
{
    public float stoppingDistance = .5f;

    private NavMeshAgent _agent;
    public NavMeshAgent Agent 
    {
        get{return _agent; } 
    }

    public override void Init()
    {
        base.Init();

        _agent = GetComponent<NavMeshAgent>();

        _agent.enabled = true;

        _agent.stoppingDistance = stoppingDistance;
    }


    protected bool IsInStoppingDistance()
    {
        return _agent.remainingDistance <= stoppingDistance;
    }

    protected bool CanMoveToDestination(Vector3 destination)
    {
        NavMeshPath path = new NavMeshPath();
        if (_agent.CalculatePath(destination, path))
        {
            if (path.status == NavMeshPathStatus.PathComplete) return true;
            else return false;
        }
        else return false;
    }

    protected void ReachedDestination()
    {
        InvokeEndEvents();
    }

    protected void InvokeStartEvents()
    {
        onMovementStartedOnce?.Invoke();
        onMovementStartedOnce = null;

        onMovementStartedAlways?.Invoke();
    }
    protected void InvokeEndEvents()
    {
        onDestinationReachedOnce?.Invoke();
        onDestinationReachedOnce = null;

        onDestinationReachedAlways?.Invoke();
    }



    public override void SetSpeed(float newValue)
    {
        base.SetSpeed(newValue);
        _agent.speed = newValue;
    }
}
