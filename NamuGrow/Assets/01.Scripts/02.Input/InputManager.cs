using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType
{
    KeyDown, 
    Key,
    KeyUp
}

// Add this magic code, you will be able to use record and init.
// I only tested in editor, not sure whether it would work on other platforms.
namespace System.Runtime.CompilerServices
{
    class IsExternalInit
    {
     
    }
}


public record InputData
{
    public KeyCode keyCode; 
    public InputType inputType;
    
    public int Age { get; init; }

    public bool Equals(KeyCode _keyCode, InputType _inputType)
    {
        return keyCode == _keyCode && inputType == _inputType;
    }
    
}

public class A
{
    public int num;
    public float fNum; 
    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    protected bool Equals(A other)
    {
        return num == other.num && fNum.Equals(other.fNum);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(num, fNum);
    }
}

public class InputManager : MonoSingleton<InputManager>
{
    public void Awake()
    {
        A a = new A();
        A b = new A();
        a.Equals(b);
    }

    private bool isCanInput = true;

    public bool IsCanInput
    {
        get
        {
            return isCanInput; 
        }
        set
        {
            isCanInput = value; 
        }
    }
    private Dictionary<InputData,bool> checkKeyCodeList = new Dictionary<InputData,bool>();

    /// <summary>
    /// InputManager에 사용할 키 등록 및 현재 상태 반환
    /// 등록되어 있지 않으면 새로 만들어서 등록
    /// InputType(KeyDown, Key, KeyUp) 상태 반환 
    /// </summary>
    /// <param name="_keyCode"></param>
    /// <param name="inputType"></param>
    /// <returns></returns>
    public bool CheckIsCanInput(KeyCode _keyCode, InputType _inputType)
    {
        bool isCan= false;
        InputData newInputData = new InputData { keyCode = _keyCode, inputType = _inputType };

        foreach (var inputData in checkKeyCodeList.Keys)
        {   
            inputData.Equals(_keyCode, _inputType);
        }
        // 현재 있는지 체크 
        if (checkKeyCodeList.TryGetValue(newInputData, out isCan) == false)
        {
            checkKeyCodeList.Add(newInputData, false);
        }

        return checkKeyCodeList[newInputData]; 
    }

    public float GetScrollWheel()
    {
        return 1f; 
    }
    private void Update()
    {
        CheckInput(); 
    }

    /// <summary>
    /// 업데이트 돌면서 키 눌렸는지 체크 
    /// </summary>
    private void CheckInput()
    {
        foreach (var keyCode in checkKeyCodeList.Keys)
        {
            //keyCod
            switch (keyCode.inputType)
            {
                case InputType.KeyDown:
                    if (Input.GetKeyDown(keyCode.keyCode))
                    {
                        checkKeyCodeList[keyCode] = true; 
                    }
                    else
                    {
                        checkKeyCodeList[keyCode] = false; 
                    }
                    break;
                case InputType.Key:
                    if (Input.GetKey(keyCode.keyCode))
                    {
                        checkKeyCodeList[keyCode] = true; 
                    }
                    else
                    {
                        checkKeyCodeList[keyCode] = false; 
                    }
                    break;
                case InputType.KeyUp:
                    if (Input.GetKeyUp(keyCode.keyCode))
                    {
                        checkKeyCodeList[keyCode] = true; 
                    }
                    else
                    {
                        checkKeyCodeList[keyCode] = false; 
                    }
                    break;
            }
             

        }
    }
    

}
