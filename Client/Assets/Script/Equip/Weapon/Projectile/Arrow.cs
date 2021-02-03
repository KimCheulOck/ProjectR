using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Weapon
{
    // 발사 타입 - 마우스로 클릭한 곳까지 (무기의 사정거리까지가 최대거리)
    // 1. 직선
    // 2. 포물선을 그리면서 
    [SerializeField]
    private GameObject objShowGroup = null;
    [SerializeField]
    private BoxCollider2D boxCollider = null;

    private float speed;
    private float range;
    private float startDelay;
    private float angle;
    private Transform arrowParent;
    private Vector2 startLocation;
    private Vector2 targetLocation;

    private void OnDisable()
    {
        Cancel();
    }

    public void SetShootData(float speed, float range, float startDelay, float angle, Transform arrowParent, Vector3 targetLocation)
    {
        this.speed = speed;
        this.range = range;
        this.startDelay = startDelay;
        this.angle = angle;
        this.arrowParent = arrowParent;
        this.targetLocation = targetLocation;
    }

    public void Shoot()
    {
        Cancel();
        actionCoroutine = StartCoroutine(Action());
    }

    public void Show()
    {
        objShowGroup.SafeSetActive(true);
        boxCollider.enabled = false;
    }

    private IEnumerator Action()
    {
        yield return new WaitForSeconds(startDelay);

        WaitForSeconds time = new WaitForSeconds(0.1f);

        objShowGroup.SafeSetActive(true);
        boxCollider.enabled = true;
        transform.position = arrowParent.position;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.localScale = arrowParent.localScale;

        startLocation = transform.position;

        while (true)
        {
            float distance = Vector2.Distance(transform.position, targetLocation);
            float rangeMax = Vector2.Distance(startLocation, transform.position);
            bool isLeft = (startLocation.x - targetLocation.x) > 0 ? true : false ;
            if ((isLeft && range <= rangeMax) || (!isLeft && range <= rangeMax))
            {
                // 최대거리에 도달
                objShowGroup.SafeSetActive(false);
                boxCollider.enabled = false;
                break;
            }

            if (distance < speed)
            {
                objShowGroup.SafeSetActive(false);
                boxCollider.enabled = false;
                break;
            }

            transform.position = Vector3.MoveTowards(transform.position, targetLocation, speed);

            yield return time;
        }
    }

    public override void Cancel()
    {
        if (actionCoroutine != null)
            StopCoroutine(actionCoroutine);

        objShowGroup.SafeSetActive(false);
        boxCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
            return;

        if (onEventHit == null)
            return;

        if (onEventHit(collision.GetComponent<Actor>()))
            Cancel();
    }
}
