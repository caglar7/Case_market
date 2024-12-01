

using System;
using UnityEngine;

public class TransformMover : BaseCharacterMover
{
    public override void Move(Vector3 moveDir)
    {
        TransformCached.position += TransformCached.right * moveDir.x * currentSpeed * Time.deltaTime;
        TransformCached.position += TransformCached.forward * moveDir.z * currentSpeed * Time.deltaTime;
    }
}