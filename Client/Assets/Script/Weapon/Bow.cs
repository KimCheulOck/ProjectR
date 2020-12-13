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

    public override void Initialize(string actorTag, Status status)
    {
        base.Initialize(actorTag, status);
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
        float range = 10.0f;
        Vector3 clickPosition = action.ClickLocation;
        int attackCount = 1;
        int loop = 0;
        while (true)
        {
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
                {
                    loop++;
                    continue;
                }

                arrow.SafeSetActive(false);
                arrowIndex = arrows.Count;
                arrows.Add(arrow);
            }

            yield return new WaitForSeconds(1.0f);

            if (loop == attackCount)
                break;
            
            arrows[arrowIndex].SafeSetActive(true);
            arrows[arrowIndex].transform.position = arrowParent.position;
            arrows[arrowIndex].transform.rotation = arrowParent.rotation;
            arrows[arrowIndex].transform.localScale = arrowParent.localScale;
            arrows[arrowIndex].Shoot(actorTag, status, 1.0f, range, clickPosition);
            loop++;
        }
    }
}
