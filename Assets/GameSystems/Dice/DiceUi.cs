using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystems.Dice
{
    public class DiceUi : MonoBehaviour, IBeginDragHandler,IDragHandler
    {
        public DiceProperties type;
        
        public void OnDrag(PointerEventData eventData)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition += eventData.delta;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
        }
    }
}