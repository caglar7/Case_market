using System.Collections;
using System.Collections.Generic;
using Template;
using TMPro;
using UnityEngine;

public class Register : BaseMono, IModuleInit
{
    public List<ItemPriceVisual> visuals = new List<ItemPriceVisual>();

    public void Init()
    {
        foreach (ItemPriceVisual visual in visuals)
        {
            visual.txt.text = visual.productType.sellPrice.ToString();
        }
    }
}


[System.Serializable]
public class ItemPriceVisual
{
    public ProductData productType;
    public TextMeshPro txt;
}