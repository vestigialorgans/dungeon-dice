using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CursorManager
{
    public class CursorManager : MonoBehaviour
    {
        [SerializeField]
        private CursorLockMode state;
        
        void Awake()
        {
            Cursor.lockState = state;
        }
    }
}
