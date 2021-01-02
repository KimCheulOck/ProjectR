using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : EquipParts
{
    public GameObject subWeapon = null;

    protected Actor actor;
    protected Action action;
    protected GameObject loadObject;
    protected int count;
    protected Coroutine actionCoroutine;

    public virtual void Initialize(Actor actor)
    {
        this.actor = actor;
    }

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
}
