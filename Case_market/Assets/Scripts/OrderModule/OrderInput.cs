


using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderInput : MonoBehaviour 
{
    public ProductData productType;
    public TextMeshProUGUI txt;

    private void OnEnable()
     {
        txt.text = "0";
    }
}