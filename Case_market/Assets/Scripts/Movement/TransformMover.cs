

using System;
using UnityEngine;

public class TransformMover : BaseCharacterMover
{
    public override void Move(Vector3 moveDir)
    {
        TransformCached.position += TransformCached.right * moveDir.x * speed * Time.deltaTime;
        TransformCached.position += TransformCached.forward * moveDir.z * speed * Time.deltaTime;
    }
}