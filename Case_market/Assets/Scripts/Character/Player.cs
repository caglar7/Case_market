using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Template;
using Unity.VisualScripting;
using UnityEngine;

public class Player : BaseCharacter ,IEvents
{
    [Header("Player")]
    public BaseMovement mover;
    public BaseInventory inventory;
    public PlayerInput playerInput;
    public CameraUnit cameraUnit;
    public Transform groundCheckPoint;

    private BaseItem _itemOnHand;
    private BaseInventory _inventoryTarget;


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
    public override void Update()
    {
        if (isUpdateActive == false) return;

        UpdateInput();
        Rotate();
        Move();
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

    private void Rotate()
    {
        // apply rotation
        TransformCached.rotation = Quaternion.Euler(0f, playerInput.BodyRotationY, 0f);
        cameraUnit.transform.localRotation = Quaternion.Euler(playerInput.CamRotationX, 0f, 0f);
    }



    private void InitComponents()
    {
        mover.Init();
        inventory.Init();
        cameraUnit.transform.localEulerAngles = Vector3.zero;
    }
    public void RegisterToEvents()
    {
        playerInput.RegisterToInputEvents();
        InputManager.instance.onKeyDown += HandleKeyDownInput;
        UIEvents.OnOpenedUI += HandleOpenedUI;
        UIEvents.OnClosedUI += HandleClosedUI;
    }

    public void UnRegisterToEvents()
    {
        playerInput.UnRegisterToInputEvents();
        InputManager.instance.onKeyDown -= HandleKeyDownInput;
        UIEvents.OnOpenedUI -= HandleOpenedUI;
        UIEvents.OnClosedUI -= HandleClosedUI;
    }

    private void HandleOpenedUI(CanvasType openedCanvas)
    {
        if(openedCanvas == CanvasType.Menu)
        {
            AdjustForUIMode();
        }
    }
    private void HandleClosedUI(CanvasType closedCanvas)
    {
        if(closedCanvas == CanvasType.Menu)
        {
            AdjustForPlayerMode();
        }
    }

    private void AdjustForUIMode()
    {
        DeActivateUpdate();
        playerInput.UnRegisterToInputEvents();
        CursorUtility.AdjustForUI();
    }
    private void AdjustForPlayerMode()
    {
        ActivateUpdate();
        playerInput.RegisterToInputEvents();
        CursorUtility.AdjustForPlayer();
    }

    public void HandleKeyDownInput(KeyCode key)
    {
        if(key == KeyCode.E)
        {
            TryHoldAndDrop();
        }

        if(key == KeyCode.Q)
        {
            UIManager.instance.SwitchCanvas(CanvasType.Menu);
        }
    }

    private void TryHoldAndDrop()
    {
        if (inventory.IsThereEmptySlot() == true)    
        {
            TryHold();
        }
        else
        {
            TryDrop();
        }
    }

    private void TryHold()
    {
        BaseItem targetItem = GetTargetItem();

        BaseInventory targetInventory = GetTargetInventory();

        switch(targetItem)
        {
            case Product product:
                TransferManager.instance.Transfer(targetItem, targetInventory, inventory, true);
                break;

            case Box box:
                inventory.TryAddItem(targetItem);
                break;
        }
    }

    private void TryDrop()
    {
        BaseItem itemHolded = inventory.LastAddedItem;

        BaseInventory targetInventory = GetTargetInventory();

        switch(itemHolded)
        {
            case Product product:
                if(targetInventory != null)
                    TransferManager.instance.Transfer(itemHolded, inventory, targetInventory, true);
                break;

            case Box box:
                DropBoxOnGround(itemHolded);
                break;
        }
    }


    private BaseItem GetTargetItem()
    {
        if (Physics.Raycast(cameraUnit.transform.position, cameraUnit.transform.forward, out RaycastHit hit, 3.5f))
        {
            if (hit.collider.TryGetComponent(out BaseItem item))
            {
                return item;
            }
        }

        return null;
    }

    private BaseInventory GetTargetInventory()
    {
        foreach (RaycastHit hit in Physics.RaycastAll(cameraUnit.transform.position, cameraUnit.transform.forward, 3.5f))
        {
            if(hit.collider.TryGetComponent(out BaseInventory target) == true)
            {
                return target;
            }
        }

        return null;
    }

    private Vector3 _boxRotation;
    private void DropBoxOnGround(BaseItem item)
    {
        foreach (RaycastHit hit in Physics.RaycastAll(groundCheckPoint.position, Vector3.down, 3f))
        {
            if(hit.collider.CompareTag("Ground") == true)
            {
                inventory.TryRemoveItem(item);

                _boxRotation = item.transform.eulerAngles;

                item.transform.DOMoveY(hit.point.y + 0.517f, .5f);

                item.transform.DORotate(new Vector3(0f, _boxRotation.y, 0f), .5f);
            }
        }
    }
}
