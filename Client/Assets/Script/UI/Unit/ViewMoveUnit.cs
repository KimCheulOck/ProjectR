using UnityEngine;
using UnityEngine.EventSystems;

public class ViewMoveUnit : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private string key = "";
    private Vector3 prevPosition = Vector3.zero;
    private bool isDrag = false;

    public void SetMoveUnit(UIPrefabs uIPrefabs)
    {
        key = uIPrefabs.ToString();

        string loadPositionString = PlayerPrefs.GetString(key, "");
        if (string.IsNullOrEmpty(loadPositionString))
            return;

        loadPositionString = loadPositionString.Replace("(", "");
        loadPositionString = loadPositionString.Replace(")", "");
        string[] positionStrings = loadPositionString.Split(',');
        Vector3 loadPosition = new Vector3(float.Parse(positionStrings[0]),
                                           float.Parse(positionStrings[1]),
                                           float.Parse(positionStrings[2]));

        transform.parent.position = loadPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그 시작
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 드래그 중
        if (!isDrag)
        {
            isDrag = true;
            prevPosition = Input.mousePosition;
            return;
        }

        transform.parent.position += (Input.mousePosition - prevPosition);
        prevPosition = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 드래그 끝 (포커스 영역 밖)
        isDrag = false;
        PlayerPrefs.SetString(key, transform.parent.position.ToString());
    }

    public void OnDrop(PointerEventData eventData)
    {
        // 드래그 끝
    }
}
