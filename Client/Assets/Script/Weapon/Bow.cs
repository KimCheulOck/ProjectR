using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    [SerializeField]
    private Transform arrowParent = null;
    [SerializeField]
    private GameObject loadPrefab = null;
    private List<Arrow> arrows = new List<Arrow>();

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
    }

    private IEnumerator Action()
    {
        Vector3 clickPosition = action.ClickLocation;
        Vector2 dir = clickPosition - actor.transform.position;
        //Vector3 angle = new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float range = 10.0f;
        int attackCount = 1;
        int loop = 0;

        while (true)
        {
            if (loop == attackCount)
                break;

            loop++;

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
                Arrow arrow = InstanceController.Create<Arrow>(loadPrefab, arrowParent);
                if (arrow == null)
                    continue;

                arrow.SafeSetActive(false);
                arrowIndex = arrows.Count;
                arrows.Add(arrow);
            }

            yield return new WaitForSeconds(1.0f);

            arrows[arrowIndex].SafeSetActive(true);
            arrows[arrowIndex].transform.position = arrowParent.position;
            arrows[arrowIndex].transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            arrows[arrowIndex].transform.localScale = arrowParent.localScale;
            arrows[arrowIndex].Shoot(actor.ActorTag, actor.Status, 1.0f, range, clickPosition);
            
        }
    }
}
