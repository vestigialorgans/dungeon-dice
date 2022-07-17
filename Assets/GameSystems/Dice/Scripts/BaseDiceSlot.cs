using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystems.Dice
{
    public class BaseDiceSlot : MonoBehaviour, IDropHandler
    {
        private DiceProperties _die;

        public bool IsUnlocked { get; set; }
        public bool CanBeRemoved { get; set; } = true;

        public DiceProperties Die
        {
            get { return _die; }
            set
            {
                _die = value;
                ReplaceChild();
            }
        }

        public int Roll()
        {
            int roll = Die.Roll();
            if (roll == 1)
            {
                Degrade();
            }

            return roll;
        }

        private void Degrade()
        {
            Die = Die.Degrade();
            ReplaceChild();
        }

        private void ReplaceChild()
        {
            if (HasSingleChild)
            {
                Destroy(transform.GetChild(0));
                InsertChild();
            }
            else if (IsEmpty)
            {
                InsertChild();
            }
        }

        private void InsertChild()
        {
            GameObject newDie = Instantiate(Die.dieUiAsset, transform.position, Quaternion.identity, transform);
        }

        private bool HasSingleChild => transform.childCount switch
        {
            1 => true,
            _ => false
        };

        private bool IsEmpty => transform.childCount == 0;
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                RectTransform targetTransform = eventData.pointerDrag.GetComponent<RectTransform>();
                eventData.pointerDrag.transform.parent = transform;
                targetTransform.anchoredPosition = Vector3.zero;
            }
        }
    }
}