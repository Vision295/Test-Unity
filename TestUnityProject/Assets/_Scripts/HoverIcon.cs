using UnityEngine;

public class HoverIcon : MonoBehaviour
{
    public Animator[] animator;
    public void EnterInIcon()
    {
        foreach(Animator item in animator)
        {
            item.SetBool("Hovering", true);
        }
    }

    public void ExitInIcon()
    {
        foreach(Animator item in animator)
        {
            item.SetBool("Hovering", false);
        }
    }
}
