

using Template;

public class ObjectCreator : Singleton<ObjectCreator> 
{
    public PoolObject product1;
    public PoolObject product2;
    public PoolObject product3;
    public PoolObject box;

    private PoolingPattern _poolCached;

    public BaseItem CreateProduct1()
    {
        return product1.poolingPattern.PullObjFromPool<Product>();
    }
    public BaseItem CreateProduct2()
    {
        return product2.poolingPattern.PullObjFromPool<Product>();
    }
    public BaseItem CreateProduct3()
    {
       return product3.poolingPattern.PullObjFromPool<Product>(); 
    }
    public void Remove(BaseItem item)
    {
        _poolCached = item.itemData.poolObject.poolingPattern;
        _poolCached.AddObjToPool(item.gameObject);
    }



    public Box CreateBox()
    {
       return box.poolingPattern.PullObjFromPool<Box>(); 
    }
    public void Remove(Box boxToRemove)
    {
        box.poolingPattern.AddObjToPool(boxToRemove.gameObject);
    }
}