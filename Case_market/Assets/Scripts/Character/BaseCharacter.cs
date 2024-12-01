

using Template;
using UnityEngine;

public abstract class BaseCharacter : BaseMono, IModuleInit
{
    [Header("Base Character")]
    public bool isUpdateActive = true;

    public abstract void Init();

    public virtual void ActivateUpdate()
    {
        isUpdateActive = true;
    }
    public virtual void DeActivateUpdate()
    {
        isUpdateActive = false;
    }
}