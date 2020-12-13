using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // 발사 타입 - 마우스로 클릭한 곳까지 (무기의 사정거리까지가 최대거리)
    // 1. 직선
    // 2. 포물선을 그리면서 
    private string actorTag;
    private Status status;
    private float speed;
    private float range;
    private Vector2 startLocation;
    private Vector2 targetLocation;

    private void OnDisable()
    {
        Cancel();
    }

    public void Shoot(string actorTag, Status status, float speed, float range, Vector3 clickPosition)
    {
        this.actorTag = actorTag;
        this.status = status;
        this.speed = speed;
        this.range = range;
        startLocation = transform.position;
        targetLocation = clickPosition;

        StartCoroutine(Action());
    }

    private IEnumerator Action()
    {
        WaitForSeconds time = new WaitForSeconds(0.1f);

        while (true)
        {
            float distance = Vector2.Distance(transform.position, targetLocation);
            float rangeMax = Vector2.Distance(startLocation, transform.position);
            bool isLeft = (startLocation.x - targetLocation.x) > 0 ? true : false ;
            if ((isLeft && range <= rangeMax) || (!isLeft && range <= rangeMax))
            {
                // 최대거리에 도달
                Debug.Log("최대거리 도착");
                this.SafeSetActive(false);
                break;
            }

            if (distance < speed)
            {
                Debug.Log("목표거리 도착");
                this.SafeSetActive(false);
                break;
            }

            transform.position = Vector3.MoveTowards(transform.position, targetLocation, speed);

            yield return time;
        }
    }

    private void Cancel()
    {
        StopCoroutine(Action());
        this.SafeSetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isAttack = (actorTag == "Player" && collision.tag == "Enemy") ||
                        (actorTag == "Enemy" && collision.tag == "Player");

        if (!isAttack)
            return;

        Actor targetActor = collision.GetComponent<Actor>();
        if (targetActor == null)
            return;

        Cancel();

        targetActor.Hit(status);
    }
}
