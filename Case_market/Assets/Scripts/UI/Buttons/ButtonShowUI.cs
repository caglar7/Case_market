


using Template;
using UnityEngine;

public class ButtonShowUI : BaseClickButton
{
    public CanvasType targetCanvas;

    public override void OnClick()
    {
        UIManager.instance.Show(targetCanvas);
    }
}