

using Template;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;

public class PosButton : BaseClickButton 
{
    public PosButtonType buttonType;
    public TextMeshProUGUI txt;

    public override void OnClick()
    {
        PosEvents.OnPosButtonPressed?.Invoke(this);
    }
}