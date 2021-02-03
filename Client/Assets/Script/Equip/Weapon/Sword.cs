using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    [SerializeField]
    private BoxCollider2D boxCollider = null;
    [SerializeField]
    private TrailRenderer trailRenderer = null;

    public override void Use(Action action)
    {
        base.Use(action);
        StartCoroutine(Action());
    }

    public override void Cancel()
    {
        StopCoroutine(Action());
        End();
    }

    private IEnumerator Action()
    {
        yield return new WaitForSeconds(0.6f);

        boxCollider.enabled = true;
        trailRenderer.enabled = true;

        yield return new WaitForSeconds(1.0f);

        End();
    }

    private void End()
    {
        boxCollider.enabled = false;
        trailRenderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
            return;

        onEventHit(collision.GetComponent<Actor>());
    }
}
