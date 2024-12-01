

using UnityEngine;
using UnityEngine.UI;

namespace Template
{
    [RequireComponent(typeof(Button))]
    public abstract class BaseButton : MonoBehaviour
    {
        [SerializeField]
        public float initDelay;

        protected Button _button;

        public Button Button
        {
            get
            {
                if(_button != null)
                    return _button;   

                else 
                {
                    _button = GetComponent<Button>();
                    return _button;
                }
            }
        }

        public virtual void EnableButton()
        {
            Button.enabled = true;
        }
        public virtual void DisableButton()
        {
            Button.enabled = false;
        }

        public virtual void FadeIn()
        {

        }
        public virtual void FadeOut()
        {

        }
    }
}