using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    public void ClickOnComputerButton(GameObject component)
    {
        SceneManager.LoadScene(component.name);
    }
}
