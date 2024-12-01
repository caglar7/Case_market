


using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour, 
                IInputMouseXY, IInputHorizontalVertical, IInputKeyDown, IInputKeyUp
{

    [Header("Settings")]
    public float mouseSensitivity = 100f;
    public float mouseLerpMult = 50f;
    public float smoothTime = 0.02f;
    public float minClampY = -90f;
    public float maxClampY = 70f;

    public Action<KeyCode> OnKeyDown, OnKeyUp;

    private float _rotIncrementX;
    private float _rotIncrementY;
    private float _horSmoothDampVelocity;
    private float _verSmoothDampVelocity;


    private float _bodyRotationY;
    public float BodyRotationY
    {
        get
        {
            return _bodyRotationY;
        }
    }


    private float _camRotationX;
    public float CamRotationX
    {
        get
        {
            return Mathf.Clamp(_camRotationX, minClampY, maxClampY);
        }
    }

    private Vector3 _moveDir;
    public Vector3 MoveDir
    {
        get
        {
            _moveDir.Normalize();
            return _moveDir;
        }
    }


    public void OnUpdate() 
    {
        _bodyRotationY += _rotIncrementX;

        _camRotationX -= _rotIncrementY;
    }




    public void RegisterToInputEvents()
    {
        InputManager.instance.onMouseXY += HandleMouseXYInput;
        InputManager.instance.onHorizontalVertical += HandleHorizontalVerticalInput;
        InputManager.instance.onKeyDown += HandleKeyDownInput;
        InputManager.instance.onKeyUp += HandleKeyUpInput;
    }

    public void UnRegisterToInputEvents()
    {
        InputManager.instance.onMouseXY -= HandleMouseXYInput;
        InputManager.instance.onHorizontalVertical -= HandleHorizontalVerticalInput;
        InputManager.instance.onKeyDown -= HandleKeyDownInput;
        InputManager.instance.onKeyUp -= HandleKeyUpInput;
    }


    public void HandleMouseXYInput(float mouseX, float mouseY)
    {
        SetIncrementSmoothDamp(mouseX, mouseY);
    }
    public void HandleHorizontalVerticalInput(float horizontal, float vertical)
    {
        _moveDir.x = horizontal;
        _moveDir.z = vertical;
    }
    public void HandleKeyDownInput(KeyCode key)
    {
        OnKeyDown?.Invoke(key);
    }
    public void HandleKeyUpInput(KeyCode key)
    {
        OnKeyUp?.Invoke(key);
    }


    private void SetIncrementDirect(float mouseX, float mouseY)
    {
        _rotIncrementX = mouseX * mouseSensitivity * Time.deltaTime;
        _rotIncrementY = mouseY * mouseSensitivity * Time.deltaTime;
    }

    private void SetIncrementLerp(float mouseX, float mouseY)
    {
        _rotIncrementX = Mathf.Lerp(_rotIncrementX, mouseX * mouseSensitivity * Time.deltaTime,
                                mouseLerpMult * Time.deltaTime);

        _rotIncrementY = Mathf.Lerp(_rotIncrementY, mouseY * mouseSensitivity * Time.deltaTime,
                                mouseLerpMult * Time.deltaTime);
    }

    private void SetIncrementSmoothDamp(float mouseX, float mouseY)
    {
        _rotIncrementX = Mathf.SmoothDampAngle(_rotIncrementX, mouseX * mouseSensitivity * Time.deltaTime,
                                            ref _horSmoothDampVelocity, smoothTime);

        _rotIncrementY = Mathf.SmoothDampAngle(_rotIncrementY, mouseY * mouseSensitivity * Time.deltaTime,
                                            ref _verSmoothDampVelocity, smoothTime);
    }

}