using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    [SerializeField]
    private Transform arrowParent = null;
    private List<Arrow> arrows = new List<Arrow>();

    private const int ARROWS_MAX = 10;

    public override void Use(Action action)
    {
        base.Use(action);
        actionCoroutine = StartCoroutine(Action());
    }

    public override void Cancel()
    {
        if(actionCoroutine != null)
            StopCoroutine(actionCoroutine);
    }

    private IEnumerator Action()
    {
        if (loadObject == null)
            yield break;

        if (count == 0)
            yield break;

        float range = 10.0f;
        int attackCount = 1;
        int loop = 0;
        while (true)
        {
            if (loop == attackCount)
                break;

            if (count == 0)
                break;

            loop++;
            count--;

            int arrowIndex = -1;
            for (int i = 0; i < arrows.Count; ++i)
            {
                if (arrows[i] == null)
                    continue;

                if (arrows[i].gameObject.activeSelf)
                    continue;

                arrowIndex = i;
                break;
            }

            if (arrowIndex == -1)
            {
                Arrow arrow = InstanceController.Create<Arrow>(loadObject, arrowParent);
                if (arrow == null)
                    continue;

                arrowIndex = arrows.Count;
                arrow.SetEvent(onEventHit, onEventActorPosition, onEventUseProjectile);
                arrows.Add(arrow);
            }

            Vector3 clickPosition = action.ClickLocation;
            Vector2 dir = clickPosition - onEventActorPosition();
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            arrows[arrowIndex].SetShootData( 1.0f, range, 1.0f, angle, arrowParent, clickPosition);
            arrows[arrowIndex].Shoot();

            yield return new WaitForSeconds(1.0f);
        }
    }
}
