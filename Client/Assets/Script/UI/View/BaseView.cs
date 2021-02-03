using UnityEngine;

public class BaseView : MonoBehaviour
{
    //private ViewFocusUnit viewFocusUnit = null;
    private ViewMoveUnit viewMoveUnit = null;
    protected UIPrefabs uiPrefabs = UIPrefabs.None;

    public void SetViewFocusUnit(UIPrefabs uiPrefabs)
    {
        this.uiPrefabs = uiPrefabs;

        viewMoveUnit = GetComponentInChildren<ViewMoveUnit>();
        if (viewMoveUnit == null)
            return;
        viewMoveUnit.SetMoveUnit(uiPrefabs);

        //viewFocusUnit = GetComponentInChildren<ViewFocusUnit>();
        //viewFocusUnit.SetHashCode(GetHashCode());
    }

    //public void ActiveFocusMask()
    //{
    //    //viewFocusUnit.SafeSetActive(true);
    //}

    //public void DisableFocusMask()
    //{
    //    //viewFocusUnit.SafeSetActive(false);
    //}
}
