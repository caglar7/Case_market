

using System.Collections;
using Sirenix.OdinInspector;
using Template;
using UnityEngine;

public class CustomerAI : BaseCharacter
{
    public AgentMoverPoint mover;
    public BaseInventory inventory;

    private int indexInQueue => CustomerManager.instance.customersInQueue.IndexOf(this);

    public Shelves _currentShelves;

    public override void Init()
    {
        TransformCached.position = Points.instance.npcSpawn.position;
        mover.Init();
        inventory.Init();

        Shelves shelves = ShelvesManager.instance.GetRandomShelves();

        if(shelves != null)
        {
            Debug.Log(transform.name + " go to shelves");
            GoToShelves(shelves);
        }
        else 
        {
            Debug.Log(transform.name + " remove try spawn again");
            ObjectCreator.instance.Remove(this);
        }
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
                BaseItem itemRemoved = inv.LastItem;

                TransferManager.instance.Transfer(itemRemoved, inv, inventory, true);
                
                collected++;

                if(firstItemCollected == false)
                {
                    firstItemCollected = true;

                    inventory.OnAnimationDone += CheckQueue;
                }

                StockEvents.OnRemoved?.Invoke(itemRemoved.itemData, 1);
            }
        }
    }

    private void CheckQueue() => StartCoroutine(CheckQueueCo());
    IEnumerator CheckQueueCo()
    {
        while(CustomerManager.instance.customersInQueue.Count >= CustomerSettings.Instance.queueLimit)
        {
            yield return 1f;
        }

        yield return 1f;
        GoToQueue();
    }


    [Button]
    private void GoToQueue()
    {
        _currentShelves.occupied = false;

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

            CustomerEvents.OnPriceCalculated += PayAndLeave;
        }
    }
    private void PayAndLeave(int payment)
    {
        if(payment >= 100)
        {
            Debug.Log("Payment is too much");
            return;
        }

        SourceManager.instance.AddSource("Money", payment);

        mover.Move(Points.instance.npcExit);

        mover.onDestinationReachedOnce += RemoveCustomer;

        CustomerManager.instance.customersInQueue.Remove(this);

        CustomerManager.instance.customersAll.Remove(this);
        
        CustomerEvents.OnCustomerLeft?.Invoke(this);
    }
    private void RemoveCustomer()
    {

        CustomerEvents.OnPriceCalculated -= PayAndLeave;

        ObjectCreator.instance.Remove(this);
    }



    private void MoveToCurrentIndexInQueue()
    {
        float normalizedPathValue = indexInQueue * (1f / CustomerSettings.Instance.queueLimit);

        Vector3 movePoint = Points.instance.path.GetPointOnPath(normalizedPathValue);

        mover.Move(movePoint);
    }


}