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

    private Weapon[] weapons = new Weapon[2];
    private StateType[] stateType = new StateType[2];
    private Equip[] equips = null;
    private Costume[] costumes = null;
    private Costume[] defaultCostumes = null;

    private System.Func<Actor, bool> onEventHit = null;
    private System.Func<Vector3> onEventActorPosition = null;
    private System.Action onEventUseProjectile = null;
    private bool isMy = false;

    public void SetMyEquip(bool isMy)
    {
        this.isMy = isMy;
    }

    public void SetEquip(Equip[] equips)
    {
        this.equips = equips;
    }

    public void SetCostume(Costume[] costumes, Costume[] defaultCostumes)
    {
        this.costumes = costumes;
        this.defaultCostumes = defaultCostumes;
    }

    public void SetEvent(System.Func<Actor, bool> onEventHit,
                         System.Func<Vector3> onEventActorPosition)
    {
        this.onEventHit = onEventHit;
        this.onEventActorPosition = onEventActorPosition;
        onEventUseProjectile = OnEventUseProjectile;
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
        if (equips == null)
            return;

        int index = (int)equipType;
        if (equips[index] == null)
        {
            if (weapons[index] == null)
                return;

            Destroy(weapons[index].gameObject);
            weapons[index] = null;
        }
        else
        {
            if (weapons[index] == null)
            {
                Weapon loadWeapone = Resources.Load<Weapon>(equips[index].Path[0]);
                weapons[index] = Instantiate(loadWeapone, equipParts[index].spriteRenderer[0].transform);
                weapons[index].SetEvent(onEventHit, onEventActorPosition, onEventUseProjectile);
            }

            if (equipType == EquipType.RightWeapon)
            {
                SetRightWeaponParts();
            }
            else if (equipType == EquipType.LeftWeapon)
            {
                SetLeftWeaponParts();
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

    private void SetRightWeaponParts()
    {
        int rightIndex = (int)EquipType.RightWeapon;
        switch (equips[rightIndex].WeaponType)
        {
            case WeaponType.Projectile:
                {
                    int leftIndex = (int)EquipType.LeftWeapon;
                    if (equips[leftIndex] == null ||
                       (equips[leftIndex].WeaponType == WeaponType.Gun &&
                        equips[leftIndex].WeaponType == WeaponType.Bow))
                    {
                        weapons[rightIndex].SetWeaponCount(0);
                        break;
                    }

                    weapons[rightIndex].SetWeaponCount(equips[rightIndex].Count);
                }
                break;

            default:
                {
                    weapons[rightIndex].SetWeaponCount(int.MaxValue);
                }
                break;
        }
    }

    private void SetLeftWeaponParts()
    {
        int leftIndex = (int)EquipType.LeftWeapon;
        switch (equips[leftIndex].WeaponType)
        {
            case WeaponType.Bow:
            case WeaponType.Gun:
                {
                    int rightIndex = (int)EquipType.RightWeapon;
                    if (weapons[rightIndex] == null)
                    {
                        weapons[leftIndex].SetLoadObject(null);
                        weapons[leftIndex].SetWeaponCount(0);
                        return;
                    }

                    weapons[leftIndex].SetLoadObject(weapons[rightIndex].subWeapon);
                    weapons[leftIndex].SetWeaponCount(equips[rightIndex].Count);
                }
                break;

            default:
                {
                    weapons[leftIndex].SetWeaponCount(int.MaxValue);
                }
                break;
        }
    }

    private void SetEquipParts(EquipType equipType)
    {
        if (equips == null)
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
        if (costumes[index] == null)
            return false;

        for (int i = 0; i < costumeParts[index].spriteRenderer.Length; ++i)
        {
            if (costumes[index].Path.Length == 0)
                continue;

            Sprite sprite = Resources.Load<Sprite>(costumes[index].Path[i]);
            costumeParts[index].spriteRenderer[i].sprite = sprite;
        }

        return false;
    }

    private bool TakeOnParts(int index)
    {
        if (equips[index] == null)
            return false;

        for (int i = 0; i < equipParts[index].spriteRenderer.Length; ++i)
        {
            if (equips[index].Path.Length == 0)
                continue;

            Sprite sprite = Resources.Load<Sprite>(equips[index].Path[i]);
            equipParts[index].spriteRenderer[i].sprite = sprite;
        }

        return true;
    }

    private bool TakeOnDefaultCostumParts(int index)
    {
        if (defaultCostumes[index] == null)
            return false;

        for (int i = 0; i < costumeParts[index].spriteRenderer.Length; ++i)
        {
            if (defaultCostumes[index].Path.Length == 0)
                continue;

            Sprite sprite = Resources.Load<Sprite>(defaultCostumes[index].Path[i]);
            costumeParts[index].spriteRenderer[i].sprite = sprite;
        }

        return false;
    }

    private bool TakeOffParts(int index)
    {
        if (equips[index] == null)
        {
            for (int i = 0; i < equipParts[index].spriteRenderer.Length; ++i)
                equipParts[index].spriteRenderer[i].sprite = null;

            return true;
        }

        return false;
    }

    private void OnEventUseProjectile()
    {
        int rightIndex = (int)EquipType.RightWeapon;
        if (equips[rightIndex].WeaponType == WeaponType.Projectile)
        {
            equips[rightIndex].SetCount(equips[rightIndex].Count - 1);
            if (equips[rightIndex].Count == 0)
            {
                if (isMy)
                    InventoryManager.Instance.RefreshInventory();

                equips[rightIndex] = null;

                SetWeaponeParts(EquipType.RightWeapon);
                SetWeaponeParts(EquipType.LeftWeapon);
            }
        }
    }
}
