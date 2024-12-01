

using Template;

public class Box : BaseItem, IModuleInit
{
    public BaseInventory inventory;

    public void Init()
    {
        // for now add 4 product 1
        inventory.Init();
    }
}
