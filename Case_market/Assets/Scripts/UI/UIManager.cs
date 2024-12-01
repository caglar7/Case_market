

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
        public CanvasType startPanel;
        private SubCanvas[] _subCanvases;
        public List<CanvasType> _currentOpenCanvases = new List<CanvasType>();

        public void Init()
        {
            _subCanvases = GetComponentsInChildren<SubCanvas>(true);
            
            SwitchCanvas(startPanel);
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
        public void Hide(CanvasType target)
        {
            foreach (SubCanvas sub in _subCanvases)
            {
                if (sub.canvasType == target)
                {
                    DeActivateSubCanvas(sub);
                }
            }

            UIEvents.OnClosedUI?.Invoke(target);
        }
        IEnumerator HideCo(CanvasType target, float initDelay)
        {
            yield return new WaitForSeconds(initDelay);

            Hide(target);
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

            _currentOpenCanvases.Remove(sub.canvasType);

            UIEvents.OnClosedUI?.Invoke(sub.canvasType);
        }

    }
}


