

using UnityEngine;

public class CustomerAI : BaseCharacter
{
    public AgentMoverPoint mover;

    public override void Init()
    {
        mover.Init();

        GoToShelves();
    }
    private void OnDisable() 
    {
            
    }


    private void GoToShelves()
    {
        mover.Move(ShelvesManager.instance.GetRandomShelves().agentWayPoint);  

        mover.onDestinationReachedOnce += CollectItemsFromShelves;  
    }

    private void CollectItemsFromShelves()
    {
        // after collected some
        GoToQueue();
    }

    private void GoToQueue()
    {
        //  move to last index and start listening queue
    }

    private void MoveInQueue()
    {

    }

    private void PayAndLeave()
    {

    }
}