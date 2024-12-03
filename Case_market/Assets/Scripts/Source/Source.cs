using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Template
{
    [CreateAssetMenu(fileName = "New Source", menuName = "New Source")]
    public class Source : ScriptableObject
    {
        [Header("Source Settings")]
        public string sourceName;
        public Sprite icon;
        public float initialValue;
        public float currentValue;
    }
}