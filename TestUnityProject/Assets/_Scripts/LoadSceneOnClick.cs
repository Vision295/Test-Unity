using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    public void ClickOnComputerButton(GameObject component)
    {
        StartCoroutine(LoadComponentScene(component));       
    }

    IEnumerator LoadComponentScene(GameObject component)
    {
        // Fade In
        FadeSystem.instance.FadeIn();
        yield return new WaitForSeconds(2f);
        // Load a scene relative to the component of the "computer"
        SceneManager.LoadScene(component.name);
    }
}
