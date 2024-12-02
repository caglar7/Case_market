


using Template;
using UnityEngine;

public class Shelves : BaseMono, IModuleInit
{
    public BaseInventory inventory1;
    public BaseInventory inventory2;
    public Transform agentWayPoint;

    public void Init()
    {
        inventory1.Init();
        inventory2.Init();
        ShelvesManager.instance.shelvesList.Add(this);
    }
}