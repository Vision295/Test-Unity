using UnityEngine;

public class HoverIcon : MonoBehaviour
{
    public Animator[] animator;
    public void EnterInIcon()
    {
        // run animation of icon in which the pointer enterred
        foreach(Animator item in animator)
        {
            // it is not the same animation for all hovering items
            item.SetBool("Hovering", true);
        }
    }

    public void ExitInIcon()
    {
        foreach(Animator item in animator)
        {
            // it is not the same animation for all hovering items
            item.SetBool("Hovering", false);
        }
    }
}
