using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeSystem : MonoBehaviour
{
    // create a singelton
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

    /*************************************************/

    void Start()
    {
        // we fade out while loading a scene
        FadeOut();
    }

    public void FadeIn()
    {
        // enable the image component because otherwise it enables all ui because is on front of it
        FadeSystem.instance.gameObject.GetComponent<Image>().enabled = true;
        FadeSystem.instance.GetComponent<Animator>().SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        StartCoroutine(FadingOut());
    }

    private IEnumerator FadingOut()
    {
        // fade out then disable the image otherwise the animation would be cancelled
        FadeSystem.instance.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(2f);
        FadeSystem.instance.gameObject.GetComponent<Image>().enabled = false;
    }
}
