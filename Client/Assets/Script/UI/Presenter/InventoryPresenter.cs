using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPresenter : BasePresenter
{
    private InventoryModel model;
    private InventoryView view;

    public InventoryPresenter(params object[] param) : base(param)
    {
        model = new InventoryModel();
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
        model.SetSlotCount();

        view = CreateView<InventoryView>();
        view.AddEvent(OnEventChangeCategory, Exit);
        view.SetSlotCount(model.inventorySlotCount, model.inventorySlotCountMax);

        int categoryIndex = PlayerPrefs.GetInt("InventoryCategory", 0);
        OnEventChangeCategory(categoryIndex);
    }

    public override UIPrefabs GetUIPrefabs()
    {
        return UIPrefabs.InventoryView;
    }

    private void OnEventChangeCategory(int categoryIndex)
    {
        model.SetCategory(categoryIndex);

        view.SetCategory(model.categoryIndex);
        view.SetSlotList(model.itemList);
    }
}