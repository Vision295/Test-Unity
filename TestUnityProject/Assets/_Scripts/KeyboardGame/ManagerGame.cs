using UnityEngine;

public class ManagerGame : MonoBehaviour
{
    // example is the input the user has to make
    char[] example = "tHis a sentence to test the game".ToCharArray();

    int advancement = 0;
    void Awake()
    {
        
    }
    
    void Update()
    {
        // We test if the list of inputs isn't empty and if the user inputs a key
        if(Input.anyKeyDown && Input.inputString.ToCharArray().Length >= 1)
        {
            Debug.Log("You have just inputed : " + Input.inputString.ToCharArray()[0] + " where you had to input " + example[advancement]);
        
            // if the user inputs the correct character
            if(Input.inputString.ToCharArray()[0] == (example[advancement]))
            {
                if(advancement < example.Length)
                {
                    advancement++;
                }
            }
        }
    }
}
