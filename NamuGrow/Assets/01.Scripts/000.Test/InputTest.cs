
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    void Update()
    {
        if (InputManager.Instance.CheckIsCanInput(KeyCode.A, InputType.KeyDown) == true)
        {
            Debug.Log("키 다운");
        }
        if (InputManager.Instance.CheckIsCanInput(KeyCode.A, InputType.Key) == true)
        {
            Debug.Log("키 누르는중");
        }
        if (InputManager.Instance.CheckIsCanInput(KeyCode.A, InputType.KeyUp) == true)
        {
            Debug.Log("키 업");
        }
        
    }
}
