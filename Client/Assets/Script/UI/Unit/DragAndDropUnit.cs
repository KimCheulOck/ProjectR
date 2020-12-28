using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropUnit : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private GameObject clone = null;
    private CanvasGroup canvasGroup = null;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그 시작
        if (clone == null)
            clone = Instantiate(gameObject, InstanceController.Instance.transform);

        canvasGroup.alpha = 0.5f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 드래그 중
        clone.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 드래그 끝 (포커스 영역 밖)
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1;
        Destroy(clone);        
    }

    public void OnDrop(PointerEventData eventData)
    {
        // 드래그 끝
        Debug.Log("OnDrop");
    }
}
