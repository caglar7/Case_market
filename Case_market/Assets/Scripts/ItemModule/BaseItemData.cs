

using UnityEngine;
using Template;

[CreateAssetMenu(fileName = "Base Item Data", menuName = "Game/Item Data/Base")]
public class BaseItemData : ScriptableObject 
{
    [Header("Base Settings")]
    public Sprite icon;
    public string itemName;
    public PoolObject poolObject;
}