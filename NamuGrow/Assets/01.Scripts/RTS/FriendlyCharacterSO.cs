using System;
using System.Globalization;
using UnityEngine;

namespace _01.Scripts.RTS
{
    [CreateAssetMenu(fileName = "Character", menuName = "FriendlyCharacterSO", order = 0)]
    public class FriendlyCharacterSO : ScriptableObject
    {
        public string name;
        
        [SerializeField]
        private GoodsType goodsType;
        public float goodsCount; 
        
        public int togetherDay;
        
        public float heath;
        public float healing;
        
        public float groundAttack;
        public float airAttack;
        public float attackSpeed;
        public float range;

        [SerializeField] private MoveType moveType;
        public int moveTypeInt = (int)moveTpye;
        public float moveSpeed;

        public string toolTip;
        
    }
}