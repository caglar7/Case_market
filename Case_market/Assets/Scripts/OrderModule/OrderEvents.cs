

using System;
using System.Collections.Generic;

public static class OrderEvents
{
    // <num of box, product list>
    public static Action<int, List<ProductData>> OnOrderCalculated; 
}