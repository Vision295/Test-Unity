using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

    void Start()
    {
        FadeOut();
    }

    public void FadeIn()
    {
        FadeSystem.instance.gameObject.GetComponent<Image>().enabled = true;
        FadeSystem.instance.GetComponent<Animator>().SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        StartCoroutine(FadingOut());
    }

    private IEnumerator FadingOut()
    {
        FadeSystem.instance.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(2f);
        FadeSystem.instance.gameObject.GetComponent<Image>().enabled = false;
    }
}
