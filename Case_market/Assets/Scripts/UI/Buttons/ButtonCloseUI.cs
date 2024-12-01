


using Template;
using UnityEngine;

public class ButtonCloseUI : BaseClickButton
{
    public CanvasType targetCanvas;

    public override void OnClick()
    {
        UIManager.instance.Hide(targetCanvas);
    }
}