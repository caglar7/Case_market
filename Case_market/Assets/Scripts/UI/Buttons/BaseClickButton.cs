


using Template;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseClickButton : BaseButton 
{
    private void Start()
    {
        Button.onClick.AddListener(() => {

            GeneralUtils.Delay(initDelay, () => OnClick());

        });
    }

    public abstract void OnClick();
}
