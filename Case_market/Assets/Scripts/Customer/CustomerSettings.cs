









using System.Collections.Generic;
using DG.Tweening;
using Template;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomerSettings", menuName = "Game/Settings/Customer Settings", order = 0)]
public class CustomerSettings : ScriptableObject
{
    private static string path = "GameSettings/CustomerSettings";

    private static CustomerSettings _instance;
    public static CustomerSettings Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = Resources.Load<CustomerSettings>(path);

                if(_instance == null)
                    Debug.LogError("not found in resources");
            }
            return _instance;
        }
    }

    [Range(1, 10)]
    public int queueLimit = 4;
}