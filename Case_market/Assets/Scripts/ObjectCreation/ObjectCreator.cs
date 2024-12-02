

using Template;

public class ObjectCreator : Singleton<ObjectCreator> 
{
    public PoolObject boxPool;
    public PoolObject customerPool;

    private PoolingPattern _poolCached;

    public BaseItem CreateProduct(ProductData productData)
    {
        _poolCached = productData.poolObject.poolingPattern;
        return _poolCached.PullObjFromPool<Product>();
    }

    public void Remove(BaseItem item)
    {
        _poolCached = item.itemData.poolObject.poolingPattern;
        _poolCached.AddObjToPool(item.gameObject);
    }


    public Box CreateBox()
    {
       return boxPool.poolingPattern.PullObjFromPool<Box>(); 
    }
    public void Remove(Box boxToRemove)
    {
        boxPool.poolingPattern.AddObjToPool(boxToRemove.gameObject);
    }


    public CustomerAI CreateCustomer()
    {
        return customerPool.poolingPattern.PullObjFromPool<CustomerAI>(); 
    }
    public void Remove(CustomerAI customerToRemove)
    {
        customerPool.poolingPattern.AddObjToPool(customerToRemove.gameObject);
    }
}