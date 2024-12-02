

using Template;
using UnityEngine;

public class Points : Singleton<Points>, IModuleInit
{
    public Transform npcSpawn;
    public Transform npcExit;
    public PathPoints path;

    public void Init()
    {
        path.Init();
    }
}
