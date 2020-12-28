using UnityEngine;

public class BaseView : MonoBehaviour
{
    private ViewFocusUnit viewFocusUnit = null;
    protected UIPrefabs uiPrefabs = UIPrefabs.None;

    public void SetViewFocusUnit(UIPrefabs uiPrefabs)
    {
        this.uiPrefabs = uiPrefabs;

        ViewFocusUnit loadPrefabs = Resources.Load<ViewFocusUnit>("Prefabs/Unit/ViewFocusUnit");
        viewFocusUnit = Instantiate(loadPrefabs, transform);
        viewFocusUnit.SetSizeOfType(uiPrefabs, GetHashCode());
    }

    public void ActiveFocusMask()
    {
        viewFocusUnit.SafeSetActive(true);
    }

    public void DisableFocusMask()
    {
        viewFocusUnit.SafeSetActive(false);
    }
}
