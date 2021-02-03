using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : EquipParts
{
    public GameObject subWeapon = null;

    protected System.Func<Actor, bool> onEventHit = null;
    protected System.Func<Vector3> onEventActorPosition = null;
    protected System.Action onEventUseProjectile = null;

    protected Action action;
    protected GameObject loadObject;
    protected int count;
    protected Coroutine actionCoroutine;

    public virtual void SetLoadObject(GameObject loadObject)
    {
        this.loadObject = loadObject;
    }

    public virtual void Use(Action action)
    {
        this.action = action;
    }

    public virtual void SetWeaponCount(int count)
    {
        this.count = count;
    }

    public virtual void Cancel()
    {
    }

    public virtual void SetEvent(System.Func<Actor, bool> onEventHit,
                         System.Func<Vector3> onEventActorPosition,
                         System.Action onEventUseProjectile)
    {
        this.onEventHit = onEventHit;
        this.onEventActorPosition = onEventActorPosition;
        this.onEventUseProjectile = onEventUseProjectile;
    }
}
