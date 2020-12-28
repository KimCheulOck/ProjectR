using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupBoxUnit : MonoBehaviour
{
    [SerializeField]
    private PopupBoxListUnit loadPrefabs = null;

    [SerializeField]
    private Grid grid = null;

    [SerializeField]
    private RectTransform objBoxBackground = null;

    [SerializeField]
    private float boxBackgroundSize = 37.0f;

    public void SetPopupBoxList()
    {
    }

    public void OpenPopupBox()
    {
        Vector2 sizeDelta = objBoxBackground.sizeDelta;
        sizeDelta.y = (boxBackgroundSize);
    }
}
