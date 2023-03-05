using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void PointerEnter(RectTransform transform)
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
    public void PointerExit(RectTransform transform)
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
}
