using System.Collections;
using System.Collections.Generic;
using Template;
using TMPro;
using UnityEngine;

public class SourceUI : MonoBehaviour
{   
    public Source data;
    public TextMeshProUGUI txt;

    private void OnEnable() {
        txt.text = data.currentValue.ToString();
    }
}
