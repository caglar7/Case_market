using System;
using System.Collections;
using System.Collections.Generic;
using Template;
using Unity.VisualScripting;
using UnityEngine;

public class Player : BaseCharacter ,IEvents
{
    [Header("Player")]
    public BaseMovement mover;
    public BaseInventory inventory;
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
        inventory.Init();
        cam.transform.localEulerAngles = Vector3.zero;
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




    public void HandleKeyDownInput(KeyCode key)
    {
        if(key == KeyCode.E)
        {
            TryCollectAndDrop();
        }
    }

    private void TryCollectAndDrop()
    {
        if (inventory.IsThereEmptySlot() == true)    
        {
            TryCollect();
        }
        else
        {
            TryDrop();
        }
    }

    private void TryCollect()
    {
        Product targetItem = null;

        BaseInventory targetInventory = null;

        foreach (RaycastHit hit in Physics.RaycastAll(cam.transform.position, cam.transform.forward, 5f))
        {
            if (hit.collider.GetComponent<Player>() != null) continue;

            if (targetItem == null) hit.collider.TryGetComponent(out targetItem);

            if (targetInventory == null) hit.collider.TryGetComponent(out targetInventory);
        }

        if (targetItem != null && targetInventory != null)
        {
            TransferManager.instance.Transfer(targetItem, targetInventory, inventory);
        }
    }

    private void TryDrop()
    {
        foreach (RaycastHit hit in Physics.RaycastAll(cam.transform.position, cam.transform.forward, 5f))
        {
            if (hit.collider.GetComponent<Player>() != null) continue;
            if (hit.collider.GetComponent<Product>() != null) continue;

            if (hit.collider.TryGetComponent(out BaseInventory targetInv) == true)
            {
                BaseItem itemHolded = inventory.LastAddedItem;

                TransferManager.instance.Transfer(itemHolded, inventory, targetInv);

                break;
            }
        }
    }


    public void HandleKeyUpInput(KeyCode key)
    {

    }



}
