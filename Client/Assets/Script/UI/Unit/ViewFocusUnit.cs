using UnityEngine;

public class ViewFocusUnit : MonoBehaviour
{
    [System.Serializable]
    private struct SizeOfType
    {
        public UIPrefabs type;
        public Vector2 size;
    }

    [SerializeField]
    private RectTransform rectTransform = null;

    [SerializeField]
    private SizeOfType[] sizeOfTypes = null;

    private UIPrefabs uiPrefabs = UIPrefabs.None;
    private long baseViewHashCode = 0;

    public void SetSizeOfType(UIPrefabs uiPrefabs, long baseViewHashCode)
    {
        this.uiPrefabs = uiPrefabs;
        this.baseViewHashCode = baseViewHashCode;

        for (int i = 0; i < sizeOfTypes.Length; ++i)
        {
            if (sizeOfTypes[i].type == uiPrefabs)
            {
                rectTransform.sizeDelta = sizeOfTypes[i].size;
                break;
            }
        }
    }

    public void OnClickUI()
    {
        UIController.FocusSort(UIController.GetPresenter(baseViewHashCode));
    }
}
