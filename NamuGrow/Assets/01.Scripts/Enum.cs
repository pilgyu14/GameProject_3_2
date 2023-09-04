using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GoodsType
{
    sap,   
}

public enum MoveType
{
    ground,
    air,
}

[Flags]
public enum TogetherTime
{
    zeroZero = 1 << 0,
    zeroOne = 1 << 1,
    zeroTwo = 1 << 2,
    zeroThree = 1 << 3,
    zeroFour = 1 << 4,
    zeroFive = 1 << 5,
    zeroSix = 1 << 6,
    zeroSeven = 1 << 7,
    zeroEight = 1 << 8,
    zeroNine = 1 << 9,
    oneZero = 1 << 10,
    oneTwo = 1 << 11,
    oneThree = 1 << 12,
    oneFour = 1 << 13,
    oneFive = 1 << 14,
    oneSix = 1 << 15,
    oneSeven = 1 << 16,
    oneEight = 1 << 17,
    oneNine = 1 << 18,
    twoZero = 1 << 19,
    twoOne = 1 << 20,
    twoTwo = 1 << 21,
    twoThree = 1 << 22,
    twoFour = 1 << 23,
}