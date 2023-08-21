using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

public record MouseInputData
{
    public KeyCode keyCode; 
    public InputType inputType;
    
    public int Age { get; init; }

    public bool Equals(KeyCode _keyCode, InputType _inputType)
    {
        return keyCode == _keyCode && inputType == _inputType;
    }
    
}

public record KeyInputData
{
    public KeyCode keyCode; 
    public InputType inputType;
    public bool isInput; 
    
    public int Age { get; init; }

    public bool Equals(KeyCode _keyCode, InputType _inputType)
    {
        return keyCode == _keyCode && inputType == _inputType;
    }
    
}

public class KeyInputData2
{
    public KeyInputData keyInputData;
    public bool isInput;

    /*public bool TryGetValue(KeyInputData _keyInputData, out bool _isInput)
    {
        return true;
    }*/
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
    private Dictionary<KeyInputData,bool> checkKeyCodeDic = new Dictionary<KeyInputData,bool>();
    private Dictionary<MouseInputData,bool> checkMouseButtonList = new Dictionary<MouseInputData,bool>();

    private List<KeyInputData2> checkKeyCodeList = new List<KeyInputData2>(); 
    
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
        KeyInputData newKeyInputData = new KeyInputData { keyCode = _keyCode, inputType = _inputType };
        
        if (checkKeyCodeDic.TryGetValue(newKeyInputData, out isCan) == false)
        {
            checkKeyCodeDic.Add(newKeyInputData, false);
            CheckInput(); 
        }
        
      //  if(checkKeyCodeList.)

        return checkKeyCodeDic[newKeyInputData]; 
    }

    /// <summary>
    /// 마우스 버튼 입력 
    /// </summary>
    /// <param name="_idx"> 0 : 좌클릭 / 1 : 우클릭 / 2 : 스크롤 클릭</param>
    /// <param name="inputType">Down Up Ing</param>
    public void GetMouseButton(int _idx, InputType inputType)
    {
        
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
        var _keys = checkKeyCodeDic.Keys; 
        foreach (var keyCode in checkKeyCodeDic.Keys.ToList())
        {
            //keyCod
            switch (keyCode.inputType)
            {
                case InputType.KeyDown:
                    if (Input.GetKeyDown(keyCode.keyCode))
                    {
                        checkKeyCodeDic[keyCode] = true; 
                    }
                    else
                    {
                        //Debug.Log(keyCode);
                        checkKeyCodeDic[keyCode] = false; 
                    }
                    break;
                case InputType.Key:
                    if (Input.GetKey(keyCode.keyCode))
                    {
                        checkKeyCodeDic[keyCode] = true; 
                    }
                    else
                    {
                        checkKeyCodeDic[keyCode] = false; 
                    }
                    break;
                case InputType.KeyUp:
                    if (Input.GetKeyUp(keyCode.keyCode))
                    {
                        checkKeyCodeDic[keyCode] = true; 
                    }
                    else
                    {
                        checkKeyCodeDic[keyCode] = false; 
                    }
                    break;
            }
             

        }
    }
    

}
