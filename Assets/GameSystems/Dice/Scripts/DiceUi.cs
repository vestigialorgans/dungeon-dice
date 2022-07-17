using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystems.Dice
{
    public class DiceUi : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler
    {
        public DiceProperties type;
        
        public BaseDiceSlot PreviousSlot { get; set; }
        public DiceManager manager;
        
        #region DragEvents
        public void OnDrag(PointerEventData eventData)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = eventData.position;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            PreviousSlot = transform.parent.GetComponent<BaseDiceSlot>();
            transform.SetParent(manager.dragCanvas.transform);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!eventData.pointerEnter.TryGetComponent<BaseDiceSlot>(out _))
            {
                transform.parent = PreviousSlot.transform;
                GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            }
        }
        #endregion
    }
}