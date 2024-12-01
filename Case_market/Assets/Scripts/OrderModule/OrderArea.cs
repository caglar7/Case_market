


using Template;
using UnityEngine;

// creating and filling the boxes with the given order

public class OrderArea : BaseMono, IModuleInit
{
    public BaseInventory inventory;

    public void Init()
    {
        inventory.Init();
    }
}