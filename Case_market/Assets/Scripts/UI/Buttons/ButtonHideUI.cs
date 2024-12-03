


using Template;
using UnityEngine;

public class ButtonHideUI : BaseClickButton
{
    public CanvasType targetCanvas;

    public override void OnClick()
    {
        UIManager.instance.Hide(targetCanvas);
    }
}