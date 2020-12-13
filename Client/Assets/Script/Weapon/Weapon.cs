using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected Action action;
    protected Status status;
    protected string actorTag;

    public virtual void Initialize(string actorTag, Status status)
    {
        this.actorTag = actorTag;
        this.status = status;
    }

    public virtual void Use(Action action)
    {
        this.action = action;
    }

    public virtual void Cancel()
    {
    }
}
