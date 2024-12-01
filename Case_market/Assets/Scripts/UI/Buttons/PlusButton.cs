


using TMPro;
using UnityEngine;

public class PlusButton : BaseClickButton
{
    public TextMeshProUGUI txt;

    public override void OnClick()
    {
        int value = int.Parse(txt.text);
        txt.text = (++value).ToString();
    }
}