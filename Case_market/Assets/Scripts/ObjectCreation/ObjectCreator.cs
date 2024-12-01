

using Template;

public class ObjectCreator : Singleton<ObjectCreator> 
{
    public PoolObject product1;
    public PoolObject product2;
    public PoolObject product3;

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
}