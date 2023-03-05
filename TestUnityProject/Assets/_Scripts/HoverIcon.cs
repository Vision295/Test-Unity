using UnityEngine;

public class HoverIcon : MonoBehaviour
{
    public Animator[] animator;
    public void EnterInIcon()
    {
        // run animation of icon in which the pointer enterred
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
