using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipPresenter : BasePresenter
{
    private EquipModel model;
    private EquipView view;

    public EquipPresenter(params object[] param) : base(param)
    {
        model = new EquipModel();
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
        view.RemoveEvent();

        base.Exit();
    }

    public override IEnumerator LoadingProcess()
    {
        yield break;
    }

    public override void LoadingEnd()
    {
        view = CreateView<EquipView>();
        view.AddEvent(OnEventChangeCategory, Exit);

        int categoryIndex = PlayerPrefs.GetInt("EquipCategory", 0);
        OnEventChangeCategory(categoryIndex);
    }

    public override UIPrefabs GetUIPrefabs()
    {
        return UIPrefabs.EquipView;
    }

    private void OnEventChangeCategory(int categoryIndex)
    {
        model.SetCategory(categoryIndex);

        view.SetCategory(model.categoryIndex);
        view.SetSlotList(model.wearEquips);
    }
}