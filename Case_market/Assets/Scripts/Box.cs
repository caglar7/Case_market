

using Template;

public class Box : BaseMono, IModuleInit
{
    public BaseInventory inventory;

    public void Init()
    {
        // for now add 4 product 1

        inventory.Init();
        inventory.TryAddItem(ObjectCreator.instance.CreateProduct2());
        inventory.TryAddItem(ObjectCreator.instance.CreateProduct2());
        inventory.TryAddItem(ObjectCreator.instance.CreateProduct3());
        inventory.TryAddItem(ObjectCreator.instance.CreateProduct1());
    }
}
