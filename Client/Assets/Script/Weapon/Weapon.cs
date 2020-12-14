using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected Actor actor;
    protected Action action;

    public virtual void Initialize(Actor actor)
    {
        this.actor = actor;
    }

    public virtual void Use(Action action)
    {
        this.action = action;
    }

    public virtual void Cancel()
    {
    }
}
