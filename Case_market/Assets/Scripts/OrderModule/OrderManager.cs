


using System.Collections.Generic;
using Template;
using UnityEngine;

public class OrderManager : Singleton<OrderManager> 
{

    public void Order(List<OrderInput> orderInputs)
    {
        foreach(var orderinput in orderInputs)
        {
            Debug.Log("type : " + orderinput.productType.ToString());


            // int.Parse(orderinput.inputField.text);
            
        }
    }
}