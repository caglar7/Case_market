using System;
using System.Collections;
using System.Collections.Generic;
using Template;
using UnityEngine;

public class Player : BaseCharacter ,IEvents
{
    [Header("Player")]
    public BaseMovement mover;
    public PlayerInput playerInput;
    public Camera cam;


    public override void Init()
    {
        CursorUtility.AdjustForPlayer();
        InitComponents();
        RegisterToEvents();
    }
    private void OnDisable()
    {
        UnRegisterToEvents();
    }
    void Update()
    {
        if (isUpdateActive == false) return;

        UpdateInput();
        Rotate();
        Move();
        Animate();
        UpdateComponents();
    }







    private void UpdateInput()
    {
        playerInput.OnUpdate();
    }
    private void UpdateComponents()
    {
    }

    private void Move()
    {
        mover.Move(playerInput.MoveDir);
        mover.OnUpdate();
    }

    private void Animate()
    {
        // Animator.SetFloat("Horizontal", playerInput.MoveDir.x);
        // Animator.SetFloat("Vertical", playerInput.MoveDir.z);
    }

    private void Rotate()
    {
        // apply rotation
        TransformCached.rotation = Quaternion.Euler(0f, playerInput.BodyRotationY, 0f);
        cam.transform.localRotation = Quaternion.Euler(playerInput.CamRotationX, 0f, 0f);
    }


    private void AdjustForUIMode()
    {
        DeActivateUpdate();
        playerInput.UnRegisterToInputEvents();
    }
    private void AdjustForPlayerMode()
    {
        ActivateUpdate();
        playerInput.RegisterToInputEvents();
    }


    private void InitComponents()
    {
        mover.Init();
        cam.transform.localEulerAngles = Vector3.zero;
    }

    public void HandleKeyDownInput(KeyCode key)
    {


    }
    public void HandleKeyUpInput(KeyCode key)
    {

    }

    public void RegisterToEvents()
    {
        playerInput.RegisterToInputEvents();
        playerInput.OnKeyDown += HandleKeyDownInput;
        playerInput.OnKeyUp += HandleKeyUpInput;
    }

    public void UnRegisterToEvents()
    {
        playerInput.UnRegisterToInputEvents();
        playerInput.OnKeyDown -= HandleKeyDownInput;
        playerInput.OnKeyUp -= HandleKeyUpInput;
    }


}
