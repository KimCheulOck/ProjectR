using UnityEngine;

public class ViewFocusUnit : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransform = null;

    private long baseViewHashCode = 0;

    public void SetHashCode(long baseViewHashCode)
    {
        this.baseViewHashCode = baseViewHashCode;
    }

    public void OnClickUI()
    {
        UIController.FocusSort(UIController.GetPresenter(baseViewHashCode));
    }
}
