using DG.Tweening;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.InventoryBase;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Grid
{
    public class DragHandler: MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private RectTransform _draggable;
        private Vector3 _draggableStartPosition;
        private CanvasGroup _canvasGroup;
        private int _index;

        public int Index => _index;

        private void Start()
        {
            _draggable = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }
        
        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            var grid = _draggable.GetComponentInParent<GridItem>();
            grid.HideCountText();
            _index = Bag.Grids.IndexOf(grid);
            _draggableStartPosition = _draggable.position;
            _canvasGroup.blocksRaycasts = false;
            grid.transform.SetAsLastSibling();
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            _draggable.DOMove(eventData.position, 0.1f);
        }
        
        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
           _draggable.DOMove(_draggableStartPosition, 0.2f);
           _canvasGroup.blocksRaycasts = true;
        }
    }
}