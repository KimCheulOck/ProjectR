using UnityEngine;
using UnityEngine.UI;

public class CategoryButtonUnit : MonoBehaviour
{
    [SerializeField]
    private Text[] txtCategoryNames = null;

    [SerializeField]
    private GameObject objOnButton = null;

    private int index;
    private string categoryName;
    private System.Action<int> callBack;

    public void SetCategory(int index, string categoryName, System.Action<int> callBack)
    {
        this.index = index;
        this.categoryName = categoryName;
        this.callBack = callBack;
    }

    public void CategoryOnOff(bool on)
    {
        objOnButton.SafeSetActive(on);
    }

    public void OnClickCategory()
    {
        CategoryOnOff(true);
        callBack(index);
    }
}
