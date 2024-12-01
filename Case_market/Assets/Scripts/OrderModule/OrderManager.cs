


using System.Collections;
using System.Collections.Generic;
using Template;
using UnityEngine;

public class OrderManager : Singleton<OrderManager> 
{

    public int _boxCount;
    public List<ProductData> _products = new List<ProductData>();

    public void Order(List<OrderInput> orderInputs)
    {
        StartCoroutine(OrderCo(orderInputs));
    }

    IEnumerator OrderCo(List<OrderInput> orderInputs)
    {
        _boxCount = 0;
        _products.Clear();

        foreach(OrderInput order in orderInputs)
        {
            for (int i = 0; i < int.Parse(order.txt.text); i++)
            {
                _products.Add(order.productType);
            }
        } 

        if((_products.Count % 4) == 0)
        {
            _boxCount = _products.Count / 4;
        }
        else
        {
            _boxCount = _products.Count / 4 + 1;
        }


        if(_products.Count > 0)
            OrderEvents.OnOrderCalculated?.Invoke(_boxCount, _products);

        yield return 0;
    }
}