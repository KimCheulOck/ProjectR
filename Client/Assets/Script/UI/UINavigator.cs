using UnityEngine;

public class UINavigator
{
    public BasePresenter Presenter { get; private set; }
    public GameObject Prefabs { get; private set; }
    public BaseView BaseView { get; private set; }

    public UINavigator(BasePresenter presenter, GameObject prefabs)
    {
        Presenter = presenter;
        Prefabs = prefabs;
        BaseView = prefabs.GetComponent<BaseView>();
        BaseView.SetViewFocusUnit(presenter.GetUIPrefabs());
    }
}