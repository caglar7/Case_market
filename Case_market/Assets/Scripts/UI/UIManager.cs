

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

/// <summary>
/// main canvas and switching methods
/// 
/// any more new subcanvas added
/// update the enum below
/// </summary>

namespace Template
{
    public class UIManager : Singleton<UIManager>, IModuleInit
    {
        [Header("General")]
        private SubCanvas[] _subCanvases;
        public List<CanvasType> _currentOpenCanvases = new List<CanvasType>();

        public void Init()
        {
            _subCanvases = GetComponentsInChildren<SubCanvas>(true);
            
            HideAll();
            Show(CanvasType.Game);
            Show(CanvasType.Cursor);
        }
        private void OnDisable() 
        {
        }


        public void SwitchCanvas(CanvasType target, float initDelay = 0f)
        {
            StartCoroutine(SwitchCanvasCo(target, initDelay));
        }

        IEnumerator SwitchCanvasCo(CanvasType target, float initDelay)
        {
            yield return new WaitForSeconds(initDelay);

            foreach(SubCanvas sub in _subCanvases)
            {
                if (sub.canvasType == target) 
                {
                    ActivateSubCanvas(sub);
                }

                else DeActivateSubCanvas(sub);
            }
        }

        public void Show(CanvasType target, float initDelay = 0f)
        {
            StartCoroutine(ShowCo(target, initDelay));
        }
        public void Show(CanvasType target)
        {
            foreach (SubCanvas sub in _subCanvases)
            {
                if (sub.canvasType == target)
                {
                    ActivateSubCanvas(sub);
                    break;
                }
            }
        }

        IEnumerator ShowCo(CanvasType target, float initDelay)
        {
            yield return new WaitForSeconds(initDelay);

            Show(target);
        }


        public void Hide(CanvasType target, float initDelay = 0f)
        {
            StartCoroutine(HideCo(target, initDelay));
        }
        IEnumerator HideCo(CanvasType target, float initDelay)
        {
            yield return new WaitForSeconds(initDelay);

            Hide(target);
        }

        public void Hide(CanvasType target)
        {
            foreach (SubCanvas sub in _subCanvases)
            {
                if (sub.canvasType == target)
                {
                    DeActivateSubCanvas(sub);
                    break;
                }
            }

            UIEvents.OnClosedUI?.Invoke(target);
        }
        public void HideAll()
        {
            foreach (SubCanvas sub in _subCanvases)
            {
                DeActivateSubCanvas(sub);
            }
        }


        private void ActivateSubCanvas(SubCanvas sub)
        {
            sub.gameObject.SetActive(true);

            _currentOpenCanvases.Add(sub.canvasType);
            
            UIEvents.OnOpenedUI?.Invoke(sub.canvasType);
        }

        private void DeActivateSubCanvas(SubCanvas sub)
        {
            sub.gameObject.SetActive(false);

            if(_currentOpenCanvases.Contains(sub.canvasType)) 
                _currentOpenCanvases.Remove(sub.canvasType);

            UIEvents.OnClosedUI?.Invoke(sub.canvasType);
        }

    }
}


