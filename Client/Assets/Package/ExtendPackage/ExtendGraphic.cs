using UnityEngine;
using UnityEngine.UI;

public static class ExtendGraphic
{
    public static void SafeSetColor(this Graphic graphic, Color color)
    {
        if (graphic == null)
            return;

        graphic.color = color;
    }

    public static void SafeSetSprite(this Image image, string path)
    {
        if (image == null)
            return;

        image.sprite = Resources.Load<Sprite>(path);
    }
}
