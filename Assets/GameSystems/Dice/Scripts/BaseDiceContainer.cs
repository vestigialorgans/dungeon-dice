using System;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace GameSystems.Dice
{
    public abstract class BaseDiceContainer : MonoBehaviour
    {
        // should be initialized in subclass
        protected BaseDiceSlot[] _slots;

        public int Roll()
        {
            int accumulator = 0;
            for (int i = 0; i < _slots.Length; i++)
            {
                BaseDiceSlot child = _slots[i];
                int currentRoll = child.Roll();
                accumulator += child.Roll();
            }

            return accumulator;
        }
        
    }
}