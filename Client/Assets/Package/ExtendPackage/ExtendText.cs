using UnityEngine.UI;

public static class ExtendText
{
    public static void SafeSetText(this Text text, string value)
    {
        if (text == null)
            return;

        text.text = value;
    }
}
