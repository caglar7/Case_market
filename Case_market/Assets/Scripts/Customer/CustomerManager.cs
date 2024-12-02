

using System.Collections.Generic;
using Sirenix.OdinInspector;
using Template;
using UnityEngine;

public class CustomerManager : Singleton<CustomerManager>, IModuleInit
{
    public List<CustomerAI> customersInQueue = new List<CustomerAI>();

    private int _index = 0;

    public void Init()
    {

    }

    [Button]
    private void SpawnCustomer()
    {
        CustomerAI customer = ObjectCreator.instance.CreateCustomer();
        customer.transform.name = "customer_" + _index++;
        customer.Init();
    }
}
