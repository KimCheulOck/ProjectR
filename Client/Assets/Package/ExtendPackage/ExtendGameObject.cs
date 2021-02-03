using UnityEngine;

public static class ExtendGameObject
{
    public static void SafeSetActive(this GameObject gameObject, bool active)
    {
        if (gameObject == null)
            return;

        gameObject.SetActive(active);
    }

    public static void SafeSetActive(this MonoBehaviour mono, bool active)
    {
        if (mono == null || mono.gameObject == null)
            return;

        mono.gameObject.SetActive(active);
    }

    public static void SafeSetActive(this Tween tween, bool active)
    {
        if (tween == null || tween.gameObject == null)
            return;

        tween.gameObject.SetActive(active);
    }

    public static void SafeSetActive(this TweenGroup tweenGroup, bool active)
    {
        if (tweenGroup == null || tweenGroup.gameObject == null)
            return;

        tweenGroup.gameObject.SetActive(active);
    }

    public static void ChangeLayer(this MonoBehaviour mono, string name, bool isChild)
    {
        if (mono == null)
            return;

        mono.gameObject.layer = LayerMask.NameToLayer(name);

        Transform[] childs = mono.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < childs.Length; ++i)
        {
            if (childs[i] == null)
                continue;

            childs[i].gameObject.layer = LayerMask.NameToLayer(name);
        }
    }
}
