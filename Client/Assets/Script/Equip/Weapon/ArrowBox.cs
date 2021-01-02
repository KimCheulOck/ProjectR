using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBox : Weapon
{
    [SerializeField]
    private Transform arrowParent = null;

    private const int ARROWS_MAX = 10;
    private Arrow arrow;

    public override void Initialize(Actor actor)
    {
        base.Initialize(actor);
        Cancel();
    }

    public override void Use(Action action)
    {
        base.Use(action);
        Cancel();
        actionCoroutine = StartCoroutine(Action());
    }

    public override void Cancel()
    {
        Debug.Log("Cancel");

        if(actionCoroutine != null)
            StopCoroutine(actionCoroutine);

        arrow.SafeSetActive(false);
    }

    private IEnumerator Action()
    {
        if (count == 0)
            yield break;

        int attackCount = 1;
        int loop = 0;
        while (true)
        {
            if (loop == attackCount)
                break;

            if (count == 0)
                yield break;

            if (arrow == null)
            {
                GameObject obj= Instantiate(subWeapon, arrowParent);
                if (obj == null)
                {
                    loop++;
                    continue;
                }

                arrow = obj.GetComponent<Arrow>();
            }

            arrow.SafeSetActive(true);
            arrow.Show();
            loop++;
            count--;

            yield return new WaitForSeconds(1.0f);
        }

        arrow.SafeSetActive(false);
    }
}
