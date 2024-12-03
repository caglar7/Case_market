


using System;
using System.Collections.Generic;
using Template;
using TMPro;
using UnityEngine;

public class PosMachine : MonoBehaviour, IModuleInit, ITriggerable, IEvents
{
    public TextMeshProUGUI txt;
    public List<char> posChars = new List<char>();
    private Player _player;



    public void Init()
    {
        RegisterToEvents();
    }
    private void OnDisable() 
    {
        UnRegisterToEvents();
    }


    public void RegisterToEvents()
    {
        PosEvents.OnPosButtonPressed += HandlePosButton;
    }

    public void UnRegisterToEvents()
    {
        PosEvents.OnPosButtonPressed -= HandlePosButton;
    }


    private void HandlePosButton(PosButton button)
    {
        switch(button.buttonType)
        {
            case PosButtonType.Number:
                AddChar(button.txt.text[0]);
                break;

            case PosButtonType.Delete:
                DeleteLastChar();
                break;

            case PosButtonType.Enter:
                Debug.Log("Price: " + txt.text);
                CustomerEvents.OnPriceCalculated?.Invoke(int.Parse(txt.text));
                break;
        }
    }

    public void TriggerEnter(BaseCharacter character)
    {
        UIManager.instance.Show(CanvasType.PosMachineGuide);

        InputManager.instance.onKeyDown += HandleKeyDown;

        _player = character.GetComponent<Player>();

        ResetPosText();
    }

    public void TriggerExit(BaseCharacter character)
    {
        UIManager.instance.Hide(CanvasType.PosMachineGuide);

        InputManager.instance.onKeyDown -= HandleKeyDown;
    }

    private bool _isOn = false;
    private void HandleKeyDown(KeyCode key)
    {
        if(key == KeyCode.R)    // pos ON
        {
            if(_isOn == false)
            {
                _isOn = true;

                _player.AdjustForUIMode();

                CameraManager.instance.CutToTarget(CameraAngleType.PosMachine);

                UIManager.instance.Show(CanvasType.PosMachinePlay);
                UIManager.instance.Hide(CanvasType.PosMachineGuide);
                UIManager.instance.Hide(CanvasType.Cursor);
            }

            else if(_isOn == true)  // pos OFF
            {
                _isOn = false;

                _player.AdjustForPlayerMode();

                CameraManager.instance.CutToTarget(CameraAngleType.Player);

                UIManager.instance.Show(CanvasType.Cursor);
                UIManager.instance.Hide(CanvasType.PosMachinePlay);
            }
        }
    }

    private void ResetPosText()
    {
        posChars.Clear();
        SetText();
    }


    private void AddChar(char c)
    {
        posChars.Add(c);
        SetText();
    }
    private void DeleteLastChar()
    {
        if(posChars.Count > 0)
        {
            posChars.RemoveAt(posChars.Count - 1);
            SetText();
        }
    }
    private void SetText()
    {
        txt.text = new string(posChars.ToArray());
    }
}