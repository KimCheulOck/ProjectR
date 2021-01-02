using UnityEngine;

public class EquipController : MonoBehaviour
{
    [System.Serializable]
    public struct Parts
    {
        public EquipType equipType;
        public SpriteRenderer[] spriteRenderer;
    }

    [SerializeField]
    private Parts[] equipParts = null;
    [SerializeField]
    private Parts[] costumeParts = null;

    private Actor actor;
    private Weapon[] weapons = new Weapon[2];
    private StateType[] stateType = new StateType[2];

    public void SetActor(Actor actor)
    {
        this.actor = actor;
    }

    public void SetParts()
    {
        SetEquipParts(EquipType.Head);
        SetEquipParts(EquipType.Body);
        SetEquipParts(EquipType.Hand);
        SetEquipParts(EquipType.Leg);

        SetCostumeParts(CostumeType.Head);
        SetCostumeParts(CostumeType.Body);
        SetCostumeParts(CostumeType.Hand);
        SetCostumeParts(CostumeType.Leg);
        SetCostumeParts(CostumeType.Hair);
        SetCostumeParts(CostumeType.Eye);
        SetCostumeParts(CostumeType.Mouse);

        SetWeaponeParts(EquipType.RightWeapon);
        SetWeaponeParts(EquipType.LeftWeapon);

        stateType[0] = StateType.None;
        stateType[1] = StateType.None;
    }

    public void SetWeaponeParts(EquipType equipType)
    {
        if (actor.Equips == null)
            return;

        int index = (int)equipType;
        if (actor.Equips[index] == null)
        {
            if (weapons[index] == null)
                return;

            Destroy(weapons[index]);
        }
        else
        {
            Weapon loadWeapone = Resources.Load<Weapon>(actor.Equips[index].Path[0]);
            weapons[index] = Instantiate(loadWeapone, equipParts[index].spriteRenderer[0].transform);
            weapons[index].Initialize(actor);

            if (actor.Equips[index].weaponType == WeaponType.Bow ||
                actor.Equips[index].weaponType == WeaponType.Gun)
            {
                int rightIndex = (int)EquipType.RightWeapon;
                if (weapons[rightIndex] == null)
                {
                    weapons[index].SetLoadObject(null);
                    weapons[index].SetWeaponCount(0);
                    return;
                }

                weapons[index].SetLoadObject(weapons[rightIndex].subWeapon);
                weapons[index].SetWeaponCount(actor.Equips[rightIndex].Count);
            }
            else if (actor.Equips[index].weaponType == WeaponType.Projectile)
            {
                weapons[index].SetWeaponCount(actor.Equips[index].Count);
            }
        }
    }

    public void UseWeapon(EquipType equipType, StateType stateType, Action action)
    {
        if (weapons == null)
            return;

        int index = (int)equipType;
        if (weapons[index] == null)
            return;

        if (stateType == StateType.Attack)
        {
            weapons[index].Use(action);
        }
        else
        {
            if (this.stateType[index] == stateType)
                return;

            weapons[index].Cancel();
        }

        this.stateType[index] = stateType;
    }

    private void SetEquipParts(EquipType equipType)
    {
        if (actor.Equips == null)
            return;

        int index = (int)equipType - (int)EquipType.Head;
        if (TakeOffParts(index))
            return;

        TakeOnParts(index);
    }

    private void SetCostumeParts(CostumeType costumeType)
    {
        // 코스튬을 입힌다.
        // 코스튬이 없으면 장비를 입힌다.
        // 장비가 없으면 기본 코스튬을 입힌다.
        // 아무것도 없으면 null
        int index = (int)costumeType;
        if (!TakeOnCostumParts(index))
        {
            if (!TakeOnDefaultCostumParts(index))
                return;
        }
    }

    private bool TakeOnCostumParts(int index)
    {
        if (actor.Costume[index] == null)
            return false;

        for (int i = 0; i < costumeParts[index].spriteRenderer.Length; ++i)
        {
            if (actor.Costume[index].Path.Length == 0)
                continue;

            Sprite sprite = Resources.Load<Sprite>(actor.Costume[index].Path[i]);
            costumeParts[index].spriteRenderer[i].sprite = sprite;
        }

        return false;
    }

    private bool TakeOnParts(int index)
    {
        if (actor.Equips[index] == null)
            return false;

        for (int i = 0; i < equipParts[index].spriteRenderer.Length; ++i)
        {
            if (actor.Equips[index].Path.Length == 0)
                continue;

            Sprite sprite = Resources.Load<Sprite>(actor.Equips[index].Path[i]);
            equipParts[index].spriteRenderer[i].sprite = sprite;
        }

        return true;
    }

    private bool TakeOnDefaultCostumParts(int index)
    {
        if (actor.DefaultCostume[index] == null)
            return false;

        for (int i = 0; i < costumeParts[index].spriteRenderer.Length; ++i)
        {
            if (actor.DefaultCostume[index].Path.Length == 0)
                continue;

            Sprite sprite = Resources.Load<Sprite>(actor.DefaultCostume[index].Path[i]);
            costumeParts[index].spriteRenderer[i].sprite = sprite;
        }

        return false;
    }

    private bool TakeOffParts(int index)
    {
        if (actor.Equips[index] == null)
        {
            for (int i = 0; i < equipParts[index].spriteRenderer.Length; ++i)
                equipParts[index].spriteRenderer[i].sprite = null;

            return true;
        }

        return false;
    }
}
