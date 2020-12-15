using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    private const int MOUSELEFT = 0;

    [SerializeField]
    private GameObject[] loadMouseClicks = null;

    private int index = 0;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(MOUSELEFT))
        {
            if (index >= loadMouseClicks.Length)
                index = 0;

            StartCoroutine(Click(index));
            index++;
        }
    }

    private IEnumerator Click(int index)
    {
        loadMouseClicks[index].SafeSetActive(true);

        yield return new WaitForSeconds(1.0f);

        loadMouseClicks[index].SafeSetActive(false);
    }
}
