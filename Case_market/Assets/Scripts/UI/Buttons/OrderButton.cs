

using System.Collections.Generic;
using UnityEngine;

public class OrderButton : BaseClickButton
{
    public List<OrderInput> orderInputs = new List<OrderInput>();

    public override void OnClick()
    {
        OrderManager.instance.Order(orderInputs);
    }
}