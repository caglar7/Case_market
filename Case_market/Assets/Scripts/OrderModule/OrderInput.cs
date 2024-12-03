


using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderInput : MonoBehaviour 
{
    public ProductData productType;
    public TextMeshProUGUI txt_Count;
    public TextMeshProUGUI txt_Price;

    private void OnEnable()
     {
        txt_Count.text = "0";
        txt_Price.text = "Unit Price: " + productType.buyPrice.ToString();
    }
}