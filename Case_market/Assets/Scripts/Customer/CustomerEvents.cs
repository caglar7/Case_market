

using System;

public static class CustomerEvents
{
    public static Action<int> OnPriceCalculated;
    public static Action<CustomerAI> OnCustomerLeft;
}