using UnityEngine;

public class HoverIcon : MonoBehaviour
{
    public Vector3 zoom;
    public void EnterInIcon(RectTransform rect)
    {
        rect.localScale = zoom;
    }

    public void ExitInIcon(RectTransform rect)
    {
        rect.localScale = new Vector3(1, 1, 1);
    }
}
