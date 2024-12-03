


using System;

public static class StockEvents
{
    public static Action<BaseItemData, int> OnAdded;
    public static Action<BaseItemData, int> OnRemoved;
}