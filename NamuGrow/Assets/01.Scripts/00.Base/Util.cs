using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util 
{
    private static Camera mainCam;

    public static Camera MainCam
    {
        get
        {
            if (mainCam == null)
            {
                mainCam = Camera.main; 
            }

            return mainCam; 
        }
    }

    public static Vector3 ClickPos
    {
        get
        {
                return MainCam.ScreenToWorldPoint(Input.mousePosition);
            
        }
    }
    
}
