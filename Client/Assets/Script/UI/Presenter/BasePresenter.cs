using System.Collections;

public abstract class BasePresenter
{
    public BasePresenter(params object[] param)
    {
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
        DeleteView();
    }

    public virtual IEnumerator LoadingProcess()
    {
        yield break;
    }

    public virtual void LoadingEnd()
    {
    }

    public virtual void RefreshUI()
    {

    }

    public abstract UIPrefabs GetUIPrefabs();

    public T CreateView<T>() where T : BaseView
    {
        return UIController.CreateUI<T>(this);
    }

    public void DeleteView()
    {
        UIController.DeleteUI(this);
    }
}
