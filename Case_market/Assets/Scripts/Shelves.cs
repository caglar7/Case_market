


using Template;
using UnityEngine;

public class Shelves : BaseMono, IModuleInit
{
    public BaseInventory inventory1;
    public BaseInventory inventory2;

    public void Init()
    {
        inventory1.Init();
        inventory2.Init();
    }
}