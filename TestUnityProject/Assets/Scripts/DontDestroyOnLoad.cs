using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public GameObject[] gameObjects;
    void Start()
    {
        foreach (GameObject item in gameObjects)
        {
            DontDestroyOnLoad(item);
        }
    }
}
