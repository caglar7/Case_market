

using Sirenix.OdinInspector;
using UnityEngine;

public class CustomerAI : BaseCharacter
{
    public AgentMoverPoint mover;

    private int indexInQueue => CustomerManager.instance.customersInQueue.IndexOf(this);

    public float normalizedPathValue;


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
        // after collected some, listen for inventory events
        GoToQueue();
    }

    [Button]
    private void GoToQueue()
    {
        CustomerManager.instance.customersInQueue.Add(this);

        MoveToCurrentIndexInQueue();

        CustomerEvents.OnCustomerLeft += MoveInQueue;

        WaitForPlayerIfFront();
    }

    private void MoveInQueue(CustomerAI customerLeft)
    {
        if(customerLeft == this) return;

        MoveToCurrentIndexInQueue();

        WaitForPlayerIfFront();
    }




    private void WaitForPlayerIfFront()
    {
        if(indexInQueue == 0)
        {
            CustomerEvents.OnCustomerLeft -= MoveInQueue;
        }

    }

    [Button]
    private void PayAndLeave()
    {
        CustomerManager.instance.customersInQueue.Remove(this);

        mover.Move(Points.instance.npcExit);// remove after seconds

        CustomerEvents.OnCustomerLeft?.Invoke(this);
    }



    private void MoveToCurrentIndexInQueue()
    {
        normalizedPathValue = indexInQueue * (1f / CustomerSettings.Instance.queueLimit);

        Vector3 movePoint = Points.instance.path.GetPointOnPath(normalizedPathValue);

        mover.Move(movePoint);
    }
}