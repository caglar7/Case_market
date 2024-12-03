

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
        StartCoroutine(SpawnLogic());
    }

    IEnumerator SpawnLogic()
    {
        while(customersAll.Count < 5)
        {
            SpawnCustomer();

            float wait = Random.Range(3f, 5f);
            
            yield return new WaitForSeconds(wait);
        }
    }

    private void SpawnCustomer()
    {
        CustomerAI customer = ObjectCreator.instance.CreateCustomer();
        customer.transform.name = "customer_" + _index++;
        customer.Init();

        customersAll.Add(customer);
    }
}
