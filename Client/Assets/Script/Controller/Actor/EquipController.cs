using UnityEngine;

public class EquipController : MonoBehaviour
{
    [System.Serializable]
    public struct EquipParts
    {
        public SpriteRenderer[] spriteRenderer;
    }

    [SerializeField]
    private EquipParts[] equipParts = null;

    private Actor actor;
    private Equip[] equips;
    private Weapon[] weapons = new Weapon[2];

    public void SetActor(Actor actor)
    {
        this.actor = actor;
    }

    public void SetEquip(Equip[] equips)
    {
        this.equips = equips;
    }

    public void SetEquipParts()
    {
        SetEquipParts(EquipType.Head);
        SetEquipParts(EquipType.Body);
        SetEquipParts(EquipType.Hand);
        SetEquipParts(EquipType.Leg);

        SetWeaponeParts(EquipType.RightWeapon);
        SetWeaponeParts(EquipType.LeftWeapon);
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

            Destroy(weapons[index]);
        }
        else
        {
            Weapon loadWeapone = Resources.Load<Weapon>(equips[index].path[0]);
            weapons[index] = Instantiate(loadWeapone, equipParts[index].spriteRenderer[0].transform);
            weapons[index].Initialize(actor);

            if (weapons[index].WeaponType == WeaponType.Bow ||
                weapons[index].WeaponType == WeaponType.Gun)
            {
                int rightIndex = (int)EquipType.RightWeapon;
                if (weapons[rightIndex] == null)
                {
                    weapons[index].SetLoadObject(null);
                    weapons[index].SetWeaponCount(0);
                    return;
                }

                weapons[index].SetLoadObject(weapons[rightIndex].subWeapon);
                weapons[index].SetWeaponCount(equips[rightIndex].Count);
            }
            else if (weapons[index].WeaponType == WeaponType.Projectile)
            {
                weapons[index].SetWeaponCount(equips[index].Count);
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

        weapons[index].Cancel();

        if (stateType == StateType.Attack)
            weapons[index].Use(action);
    }

    private void SetEquipParts(EquipType equipType)
    {
        if (equips == null)
            return;

        int index = (int)equipType;
        if (TakeOffParts(index))
            return;

        TakeOnParts(index);
    }

    private void TakeOnParts(int index)
    {
        for (int i = 0; i < equipParts[index].spriteRenderer.Length; ++i)
        {
            if (equips[index].path.Length == 0)
                continue;

            Sprite sprite = Resources.Load<Sprite>(equips[index].path[i]);
            equipParts[index].spriteRenderer[i].sprite = sprite;
        }
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
}
