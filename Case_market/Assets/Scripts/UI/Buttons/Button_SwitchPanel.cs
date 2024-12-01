


using UnityEngine;


namespace Template
{
    public class Button_SwitchPanel : BaseClickButton
    {
        public CanvasType targetPanel;

        public override void OnClick()
        {
            UIManager.instance.SwitchCanvas(targetPanel);
        }
    }
}