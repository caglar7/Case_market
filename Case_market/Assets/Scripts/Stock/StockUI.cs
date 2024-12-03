

using TMPro;
using UnityEngine;

public class StockUI : MonoBehaviour 
{
    public TextMeshProUGUI txt;
    public BaseItemData itemType;

    private void OnEnable() 
    {
        txt.text = itemType.itemName + " stock: " + StockManager.instance.GetStockCount(itemType);
    }    
}