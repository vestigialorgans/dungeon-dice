using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.Dice
{
    public class HealthDiceContainer : BaseDiceContainer
    {
        void Start()
        {
            List<BaseDiceSlot> validChildren = new List<BaseDiceSlot>();
            foreach (Transform child in transform)
            {
                if (child.TryGetComponent(out BaseDiceSlot slot)) validChildren.Add(slot);
            }

            _slots = validChildren.ToArray();
        }
        
        
    }
}