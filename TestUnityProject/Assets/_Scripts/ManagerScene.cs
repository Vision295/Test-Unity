using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{
    public void ClickOnComputerButton(GameObject component)
    {
        StartCoroutine(LoadSceneOnClick(component));       
    }

    IEnumerator LoadSceneOnClick(GameObject component)
    {
        // Fade In
        FadeSystem.instance.FadeIn();
        yield return new WaitForSeconds(1f);
        // Load a scene relative to the component of the "computer"
        SceneManager.LoadScene(component.name);
    }
}
