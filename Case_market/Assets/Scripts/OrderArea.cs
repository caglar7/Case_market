


using Template;
using UnityEngine;

public class OrderArea : BaseMono, IModuleInit
{
    public BaseInventory inventory;

    public void Init()
    {
        inventory.Init();
    }
}