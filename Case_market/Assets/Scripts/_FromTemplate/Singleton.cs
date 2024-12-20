using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Template
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T instance { get; private set; }

        protected virtual void Awake()
        {
            if (instance == null)
                instance = GetComponent<T>();
            else if (instance != this)
                Destroy(gameObject);
        }
    }
}
