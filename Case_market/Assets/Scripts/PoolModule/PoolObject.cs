

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Template
{
    [CreateAssetMenu(fileName = "New Pool Object", menuName = "New Pool Object")]
    public class PoolObject : ScriptableObject
    {
        public GameObject obj;
        public string poolName;
        public int poolCount;
        [HideInInspector] public PoolingPattern poolingPattern;
    }
}