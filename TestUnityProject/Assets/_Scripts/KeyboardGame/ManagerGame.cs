using UnityEngine;
using TMPro;

public class ManagerGame : MonoBehaviour
{
    // example is the input the user (chain of character)
    private char[] example = "Ceci est le premier exemple tu dois essayer d'écrire ce qui est affiché. Bon courage !".ToCharArray();
    // string later converted into a chain of character
    public string display = "Ceci est le premier ";

    // advancement in the example (index of the character are we at)
    int advancement = 0;
    TMP_Text text;
    void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    string FillDisplay(char[] _display, char _nextCharacter)
    {
        /*
            function used to delete the first character of a string (_display) 
            and move all character one step to the left to leave an open space 
            for a new character (_nextCharacter)
        */
        // make a chain of character as long as the original
        char[] m_display  = new char[_display.Length];

        // loop throw the arrays
        for(int i = 0; i < _display.Length - 1; i++)
        {
            m_display[i] = _display[i + 1];
        }

        // insert last character
        m_display[display.Length - 1] = _nextCharacter;

        return new string(m_display);
    }
    
    void Update()
    {
        // We test if the list of inputs isn't empty and if the user inputs a key
        if(Input.anyKeyDown && Input.inputString.Length >= 1)
        {
            // test if index ot out of range
            if(advancement < example.Length - display.Length)
            {
                // if the user inputs the same character as the example
                if(Input.inputString[0] == (example[advancement]))
                {
                    advancement++;
                    // display : string --> char[]
                    display = FillDisplay(display.ToCharArray(), example[display.Length + advancement - 1]);
                } 
            }
            // same : if index not out of range
            else if(advancement < example.Length)
            {
                if(Input.inputString[0] == (example[advancement]))
                {
                    advancement++;
                    // display : string --> char[]
                    display = FillDisplay(display.ToCharArray(), ' ');
                } 
            }
        }

        text.text = display;
        
    }
}
