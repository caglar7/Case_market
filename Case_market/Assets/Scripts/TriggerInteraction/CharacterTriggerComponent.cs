using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Template
{
    public class CharacterTriggerComponent : MonoBehaviour
    {
        public readonly List<Transform> items = new List<Transform>();
        private BaseCharacter _character;

        private void Awake()
        {
            _character = GetComponentInParent<BaseCharacter>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var newTransform = other.transform;
            if (!items.Contains(newTransform))
            {
                OnInteractStart(newTransform);
                items.Add(other.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var newTransform = other.transform;
            if (items.Contains(newTransform))
            {
                OnInteractEnd(newTransform);
                items.Remove(other.transform);
            }
        }
        
        protected virtual void OnInteractStart(Transform interact)
        {
            if (interact.TryGetComponent(out ITriggerable interactAction))
            {
                interactAction.TriggerEnter(_character);
            }
        }

        protected virtual void OnInteractEnd(Transform interact)
        {
            if (interact.TryGetComponent(out ITriggerable interactAction))
            {
                interactAction.TriggerExit(_character);
            }
        }
    }
}