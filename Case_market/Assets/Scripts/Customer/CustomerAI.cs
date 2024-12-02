

using Sirenix.OdinInspector;
using UnityEngine;

public class CustomerAI : BaseCharacter
{
    public AgentMoverPoint mover;
    public BaseInventory inventory;

    private int indexInQueue => CustomerManager.instance.customersInQueue.IndexOf(this);

    public Shelves _currentShelves;

    public override void Init()
    {
        mover.Init();
        inventory.Init();

        Shelves shelves = ShelvesManager.instance.GetRandomShelves();

        if(shelves != null)
            GoToShelves(shelves);
        else 
            ObjectCreator.instance.Remove(this);
    }

    private void OnDisable() 
    {

    }
    public override void Update()
    {
        mover.OnUpdate();
    }



    private void GoToShelves(Shelves shelves)
    {
        shelves.occupied = true;

        mover.Move(shelves.agentWayPoint);

        mover.onDestinationReachedOnce += CollectItemsFromShelves;  

        _currentShelves = shelves;
    }

    [Button]
    private void CollectItemsFromShelves()
    {
        int collectCount = Random.Range(1, 4);

        int collected = 0;

        bool firstItemCollected = false;

        collectCount = Mathf.Clamp(collectCount, 1, _currentShelves.GetItemCount());

        foreach (BaseInventory inv in _currentShelves.inventoryList)
        {
            while(collected < collectCount && inv.IsThereAnyItem() == true)
            {
                TransferManager.instance.Transfer(inv.LastItem, inv, inventory, true);
                collected++;

                if(firstItemCollected == false)
                {
                    firstItemCollected = true;

                    inventory.OnAnimationDone += GoToQueue;
                }
            }
        }
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
        float normalizedPathValue = indexInQueue * (1f / CustomerSettings.Instance.queueLimit);

        Vector3 movePoint = Points.instance.path.GetPointOnPath(normalizedPathValue);

        mover.Move(movePoint);
    }


}