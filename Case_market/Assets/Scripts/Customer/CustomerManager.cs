

using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Template;
using UnityEngine;

public class CustomerManager : Singleton<CustomerManager>, IModuleInit
{
    public List<CustomerAI> customersInQueue = new List<CustomerAI>();

    public List<CustomerAI> customersAll = new List<CustomerAI>();

    private int _index = 0;

    public void Init()
    {
        InvokeRepeating("TrySpawnCustomer", 2f, 4f);
    }


    private void TrySpawnCustomer()
    {
        if(customersAll.Count >= 5) return;

        CustomerAI customer = ObjectCreator.instance.CreateCustomer();

        customer.transform.name = "customer_" + _index++;
        
        customer.Init();

        customersAll.Add(customer);
    }
}
