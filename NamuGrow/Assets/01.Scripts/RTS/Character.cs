using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace _01.Scripts.RTS
{
    public class FriendlyCharacter : MonoBehaviour
    {
        [SerializeField] private FriendlyCharacterSO SO;

        private string name;
        private GoodsType goodsType;
        private float goodsCount;

        private int togetherDay;
        
        
        private float heath;
        private float healing;
        
        private float groundAttack;
        private float airAttack;
        private float attackSpeed;
        private float range;

        private int a = (int)MoveType.ground;
        private float moveSpeed;

        private string toolTip;
        
        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            SO.name = name;
            SO.goodsCount = goodsCount;
            SO.togetherDay = togetherDay;
        }
    }
}