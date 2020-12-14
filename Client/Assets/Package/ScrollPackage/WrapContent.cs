using UnityEngine;
using UnityEngine.UI;

public class WrapContent : MonoBehaviour
{
    public enum Movement
    {
        Horizontal = 0,
        Vertical = 1,
    }

    //public enum ScrollRect
    //{
    //    LeftTop,
    //    Top,
    //    RightTop,
    //    Left,
    //    Center,
    //    Right,
    //    LeftBottom,
    //    Bottom,
    //    RightBottom,
    //}

    [Header("방향")]
    [SerializeField]
    private Movement movement = Movement.Horizontal;

    //[Header("하위 엥커 보정")]
    //[SerializeField]
    //private ScrollRect scrollRect = ScrollRect.LeftTop;

    [Header("아이템 개수")]
    [SerializeField]
    private int itemCount = 0;

    [Header("아이템 간격")]
    [SerializeField]
    private float itemSize = 100;

    [Header("아이템 동적 간격")]
    [SerializeField]
    private bool reSizeScroll = false;

    [Header("아이템 동적 간격")]
    [SerializeField]
    private float reSizeMinSize = 0;

    public delegate void OnUpdateItem(int realIndex, GameObject child);
    private OnUpdateItem onUpdateItem;
    private RectTransform[] childObject;
    private RectTransform rectTransform;
    private Vector3 previuesPosition;
    private Vector2 anchorsPivotVector2 = new Vector2(0.5f, 1.0f);
    private float ComperePosition { get { return childIndex.Length == 0 ? 0 :(childIndex[0] * itemSize) + (itemSize * 1.5f); } }
    private int HeadIndex { get { return childIndex[0]; } }
    private int TailIndex { get { return childIndex[childIndex.Length - 1]; } }
    private int[] childIndex;
    private bool update = false;

    private ScrollRect scrollRect = null;

    private System.Collections.Generic.Dictionary<int, float> reSizeRecode = new System.Collections.Generic.Dictionary<int, float>();

#if UNITY_EDITOR
    private void Awake()
    {
        TestSetting();
    }
#endif

    [ContextMenu("테스트")]
    public void TestSetting()
    {
        SetChildItem();
        SetItemCount(itemCount);
        SetUpdateEvent(null);
        ResetPosition();
    }

    public void LoadNeedCountChildPrefabs(GameObject loadPrefabs, int defaultCount)
    {
        RectTransform parentTransform = (RectTransform)transform.parent;
        Rect parentRect = parentTransform.rect;
        int childCount = transform.childCount;
        int needChildCount = movement == Movement.Horizontal ? (int)(parentRect.width/ itemSize) + 2 : (int)(parentRect.height / itemSize) + 2;

        if (needChildCount < defaultCount)
            needChildCount = defaultCount;

        if (childCount < needChildCount)
        {
            int length = needChildCount - childCount;
            for (int i = 0; i < length; ++i)
                Instantiate(loadPrefabs, transform);
        }
    }
    
    public void SetItemCount(int count)
    {
        itemCount = count;
        reSizeRecodeList = new float[itemCount];

        rectTransform = GetComponent<RectTransform>();
        SetAnchorPivot();

        Vector2 delta = rectTransform.sizeDelta;
        float itemSizeValue = (itemSize * itemCount);
        delta.x = movement == Movement.Horizontal ? itemSizeValue : delta.x;
        delta.y = movement == Movement.Vertical ? itemSizeValue : delta.y;

        rectTransform.sizeDelta = delta;
        //rectTransform.anchoredPosition = new Vector2(delta.x, delta.y);
    }

    public void SetUpdateEvent(OnUpdateItem updateEvent)
    {
        onUpdateItem = updateEvent;
    }

    public void ResetPosition()
    {
        transform.localPosition = Vector3.zero;
        previuesPosition = Vector3.zero;
        scrollRect = transform.parent.parent.GetComponent<ScrollRect>();

        update = false;

        for (int i = 0; i < childObject.Length; ++i)
            MoveChildObject(i, i);

        for (int i = 0; i < childIndex.Length; ++i)
            childIndex[i] = i;
    }

    public void UpdatePosition()
    {
        int tailIndex = TailIndex;
        int loopCount = 0;
        while (true)
        {
            if (loopCount == childIndex.Length)
                break;

            int nextObjectIndex = tailIndex < childIndex.Length ? tailIndex : (tailIndex % childIndex.Length);
            MoveChildObject(tailIndex, nextObjectIndex);

            tailIndex--;
            loopCount++;
        }
    }

    [ContextMenu("EndPosition")]
    public void EndPosition()
    {
        if (scrollRect == null)
            return;

        float endPosition = (itemCount * itemSize) - scrollRect.preferredHeight;
        if (movement == Movement.Horizontal)
            rectTransform.anchoredPosition = new Vector2(Mathf.Max(0, endPosition), 0);
        else
            rectTransform.anchoredPosition = new Vector2(0, Mathf.Max(0, endPosition));

        update = true;
        previuesPosition = transform.localPosition;

        int loopCount = 0;
        int startIndex = itemCount - 1;
        int endIndex = itemCount <= childObject.Length ? itemCount : Mathf.Abs(itemCount % childObject.Length);
        endIndex = endIndex - 1;

        while (true)
        {
            if (loopCount == childObject.Length)
                break;

            if (startIndex < 0)
                startIndex = childObject.Length - 1;

            if (endIndex < 0)
                endIndex = childIndex.Length - 1;

            childIndex[endIndex] = startIndex;

            startIndex--;
            endIndex--;
            loopCount++;
        }

        for (int i = 0; i < childIndex.Length; ++i)
        {
            MoveChildObject(childIndex[i], i);
        }
    }

    public void SetChildItem()
    {
        childIndex = new int[transform.childCount];
        childObject = new RectTransform[childIndex.Length];
        for (int i = 0; i < childIndex.Length; ++i)
        {
            childObject[i] = transform.GetChild(i) as RectTransform;

            SetChildAnchorPivot(i);
        }

        //reSizeRecodeList.Clear();
        //reSizeRecode.Clear();
    }

    private void Update()
    {
        if (update && transform.localPosition == previuesPosition)
            return;

        update = true;

        if (movement == Movement.Horizontal)
        {
            CheckHeadToTail(previuesPosition.x, transform.localPosition.x);
            CheckTailToHead(previuesPosition.x, transform.localPosition.x);
        }
        else
        {
            CheckHeadToTail(previuesPosition.y, transform.localPosition.y);
            CheckTailToHead(previuesPosition.y, transform.localPosition.y);
        }

        previuesPosition = transform.localPosition;
    }

    private void CheckHeadToTail(float previuesPosition, float currentPosition)
    {
        if (previuesPosition >= currentPosition || currentPosition <= ComperePosition)
            return;

        if (TailIndex >= (itemCount - 1))
            return;

        int nextIndex = TailIndex + 1;
        int nextObjectIndex = Mathf.Abs(HeadIndex < childIndex.Length ? HeadIndex : (HeadIndex % childIndex.Length));

        MoveChildObject(nextIndex, nextObjectIndex);

        for (int i = (childIndex.Length - 1); i >= 0; --i)
            childIndex[i] = nextIndex--;
    }

    private void CheckTailToHead(float previuesPosition, float currentPosition)
    {
        if (previuesPosition <= currentPosition || currentPosition >= ComperePosition)
            return;

        if (HeadIndex <= 0)
            return;

        int nextIndex = HeadIndex - 1;
        int nextObjectIndex = TailIndex < childIndex.Length ? TailIndex : (TailIndex % childIndex.Length);

        MoveChildObject(nextIndex, nextObjectIndex);

        for (int i = 0; i < childIndex.Length; ++i)
            childIndex[i] = nextIndex++;
    }

    private void MoveChildObject(int nextIndex, int nextObjectIndex)
    {
        if (nextObjectIndex < 0 || nextObjectIndex >= childObject.Length)
            return;

        if (childObject[nextObjectIndex] == null)
            return;

        childObject[nextObjectIndex].name = nextIndex.ToString();

        if (nextIndex >= 0 && nextIndex < itemCount)
        {
            childObject[nextObjectIndex].gameObject.SafeSetActive(true);
            SetChildObjectPosition(nextIndex, nextObjectIndex);

            if (onUpdateItem == null)
                return;

            onUpdateItem(nextIndex, childObject[nextObjectIndex].gameObject);
        }
        else
        {
            childObject[nextObjectIndex].gameObject.SafeSetActive(false);
            SetChildObjectPosition(nextIndex, nextObjectIndex);
        }
    }

    public void SetChildObjectPosition(int nextIndex, int nextObjectIndex)
    {
        if (reSizeScroll)
        {
            RectTransform[] childRectTransform = childObject[nextObjectIndex].GetComponentsInChildren<RectTransform>(false);
            if (childRectTransform == null)
                return;

            float size = 0;
            float startPosition = 0;
            float endPosition = 0;
            for (int i = 0; i < childRectTransform.Length; ++i)
            {
                if (childRectTransform[i] == null)
                    continue;

                if (!childRectTransform[i].gameObject.activeInHierarchy)
                    continue;

                if (i == 0)
                    startPosition = childRectTransform[i].position.y;
                else
                    startPosition = Mathf.Min(startPosition, childRectTransform[i].position.y);

                endPosition = Mathf.Max(endPosition, childRectTransform[i].position.y + childRectTransform[i].sizeDelta.y);
            }

            size = endPosition - startPosition;

            float prevReSizeGap = 0;
            for (int i = 0; i < nextIndex; ++i)
            {
                if (i < reSizeRecodeList.Length)
                    prevReSizeGap += reSizeRecodeList[i];
            }

            float gap = size - reSizeMinSize;
            if (nextIndex < reSizeRecodeList.Length)
            {
                if (reSizeRecodeList[nextIndex] == 0)
                {
                    Vector2 delta = rectTransform.sizeDelta;
                    float itemSizeValue = (itemSize * itemCount);
                    delta.x = movement == Movement.Horizontal ? delta.x + gap : delta.x;
                    delta.y = movement == Movement.Vertical ? delta.y + gap : delta.y;
                    rectTransform.sizeDelta = delta;
                }

                reSizeRecodeList[nextIndex] = gap;
            }

            float pos = 0;
            if (nextIndex > 0)
            {
                float defaultSize = itemSize * nextIndex;
                pos = Mathf.Max(defaultSize, defaultSize + prevReSizeGap);
            }

            childObject[nextObjectIndex].anchoredPosition = movement == Movement.Horizontal ?
                new Vector2(pos, 0) : new Vector2(0, -pos);
        }
        else
        {
            float pos = (itemSize * nextIndex);
            childObject[nextObjectIndex].anchoredPosition = movement == Movement.Horizontal ? 
                new Vector2(pos, 0) : new Vector2(0, -pos);
        }
    }

    private float[] reSizeRecodeList = null;

    public void SetAnchorPivot()
    {
        if (movement == Movement.Horizontal)
        {
            rectTransform.anchorMax = Vector2.up; 
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.pivot = Vector2.up;
        }
        else
        {
            rectTransform.anchorMax = Vector2.one;
            rectTransform.anchorMin = Vector2.up;
            rectTransform.pivot = Vector2.up;
        }
    }

    public void SetChildAnchorPivot(int index)
    {
        if (movement == Movement.Horizontal)
        {
            childObject[index].anchorMax = Vector2.up;
            childObject[index].anchorMin = Vector2.up;
            childObject[index].pivot = Vector2.up;
        }
        else
        {
            childObject[index].anchorMax = anchorsPivotVector2;
            childObject[index].anchorMin = anchorsPivotVector2;
            childObject[index].pivot = anchorsPivotVector2;
        }
    }
}
