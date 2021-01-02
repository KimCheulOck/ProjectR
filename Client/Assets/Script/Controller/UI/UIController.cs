using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class UIController : MonoSingleton<UIController>
{
    [SerializeField]
    private Transform uiParent = null;

    private List<UINavigator> navigator = new List<UINavigator>();

    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(gameObject);
    }

    public static void Enter(BasePresenter presenter)
    {
        Instance.StartCoroutine(Instance.EnterProcessing(presenter));
    }

    private IEnumerator EnterProcessing(BasePresenter presenter)
    {
        presenter.Enter();

        yield return presenter.LoadingProcess();

        presenter.LoadingEnd();
    }

    public static void Exit(BasePresenter presenter)
    {
        presenter.Exit();
    }

    /// <summary>
    /// Presenter 외에서 절대 호출하지 말아야한다.
    /// </summary>
    public static T CreateUI<T>(BasePresenter presenter)
    {
        string prefabsName = string.Format("Prefabs/View/{0}", presenter.GetUIPrefabs().ToString());
        MonoBehaviour prefabs = Instantiate(Resources.Load<MonoBehaviour>(prefabsName), Instance.uiParent);
        if (prefabs == null)
        {
            Debug.LogError("Error!! Not Find of PrefabsName : {0}", prefabsName);
            return default(T);
        }

        prefabs.transform.SetParent(Instance.uiParent);
        Instance.navigator.Add(new UINavigator(presenter, prefabs.gameObject));

        Instance.SetFocusMask();

        return prefabs.GetComponent<T>();
    }

    public static T FindUI<T>(BasePresenter presenter)
    {
        UINavigator findNavigator = Instance.navigator.Find(data => data.Presenter == presenter);
        if (findNavigator == null)
            return default(T);

        return findNavigator.Prefabs.GetComponent<T>();
    }

    public static bool IsOpenUI()
    {
        return Instance.navigator.Count > 0;
    }

    public static bool IsOpenVIew(UIPrefabs uiPrefabs)
    {
        return GetPresenter(uiPrefabs) == null ? false : true;
    }

    public static BasePresenter GetPresenter(UIPrefabs uiPrefabs)
    {
        if (Instance.navigator == null || Instance.navigator.Count == 0)
            return null;

        for (int i = Instance.navigator.Count - 1; i >= 0; --i)
        {
            if (Instance.navigator[i].Presenter.GetUIPrefabs() == uiPrefabs)
                return Instance.navigator[i].Presenter;
        }

        return null;
    }

    public static BasePresenter GetPresenter(long hashCode)
    {
        if (Instance.navigator == null || Instance.navigator.Count == 0)
            return null;

        for (int i = Instance.navigator.Count -1; i >= 0; --i)
        {
            if (Instance.navigator[i].BaseView.GetHashCode() == hashCode)
                return Instance.navigator[i].Presenter;
        }

        return null;
    }

    public static void DeleteUI(BasePresenter presenter)
    {
        for (int i = 0; i < Instance.navigator.Count; ++i)
        {
            if (Instance.navigator[i] == null)
                continue;

            if (Instance.navigator[i].Presenter.GetHashCode() == presenter.GetHashCode())
            {
                Destroy(Instance.navigator[i].Prefabs);
                Instance.navigator.RemoveAt(i);
                Instance.SetAllFocusMask();
                break;
            }
        }
    }

    public static void FocusSort(BasePresenter presenter)
    {
        for (int i = 0; i < Instance.navigator.Count; ++i)
        {
            if (Instance.navigator[i] == null)
                continue;

            if (Instance.navigator[i].Presenter.GetHashCode() == presenter.GetHashCode())
            {
                UINavigator navigator = Instance.navigator[i];
                Instance.navigator.RemoveAt(i);
                Instance.navigator.Add(navigator);
                Instance.SetAllFocusMask();

                navigator.Prefabs.transform.SetAsLastSibling();
                break;
            }
        }
    }

    private void SetAllFocusMask()
    {
        for (int i = 0; i < navigator.Count; ++i)
            navigator[i].BaseView.ActiveFocusMask();

        int currentIndex = navigator.Count - 1;
        if (currentIndex >= 0)
            navigator[currentIndex].BaseView.DisableFocusMask();
    }

    private void SetFocusMask()
    {
        int currentIndex = navigator.Count - 1;
        int prevIndex = currentIndex - 1;

        if (prevIndex >= 0)
            navigator[prevIndex].BaseView.ActiveFocusMask();

        navigator[currentIndex].BaseView.DisableFocusMask();
    }
}
