using UnityEngine;

public class FadeSystem : MonoBehaviour
{
    public static FadeSystem instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void FadeIn()
    {
        FadeSystem.instance.GetComponent<Animator>().SetTrigger("FadeIn");
    } 
}
