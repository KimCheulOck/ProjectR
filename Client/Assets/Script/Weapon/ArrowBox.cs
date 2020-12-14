using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBox : Weapon
{
    [SerializeField]
    private Transform boxParent = null;
    [SerializeField]
    private Transform arrowParent = null;
    [SerializeField]
    private GameObject loadPrefab = null;
    private Arrow arrow;

    private const int ARROWS_MAX = 10;

    public override void Initialize(Actor actor)
    {
        base.Initialize(actor);
        Cancel();
    }

    public override void Use(Action action)
    {
        base.Use(action);
        StartCoroutine(Action());
    }

    public override void Cancel()
    {
        StopCoroutine(Action());
        arrow.SafeSetActive(false);
    }

    private IEnumerator Action()
    {
        int attackCount = 1;
        int loop = 0;
        while (true)
        {
            if (loop == attackCount)
                break;

            if (arrow == null)
            {
                GameObject obj= Instantiate(loadPrefab, arrowParent);
                if (obj == null)
                {
                    loop++;
                    continue;
                }

                arrow = obj.GetComponent<Arrow>();
            }

            arrow.SafeSetActive(true);
            loop++;

            yield return new WaitForSeconds(1.0f);
        }

        arrow.SafeSetActive(false);
    }
}
