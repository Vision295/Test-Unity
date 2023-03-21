using UnityEngine;
using TMPro;
using System.Linq;

public class ManagerGame : MonoBehaviour
{
    // example is the input the user (chain of character)
    private char[] example = "Ceci est le premier exemple tu dois essayer d'écrire ce qui est affiché. Bon courage !".ToCharArray();
    // string later converted into a chain of character
    public string display = "Ceci est le premier ";
    public GameObject[] keyboard;

    // advancement in the example (index of the character are we at)
    int advancement = 0;
    TMP_Text text;

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

    void HighLight(char _nextCharacter, bool _ending = false)
    {
        /*
            function used to highlight (make it bigger) the key associated to the 
            character (_nextCharacter) supposed to be written
        */

        // to exclude the name of certain objects
        bool error = false;
        int maj = (int)_nextCharacter;
        if(
            (maj > 65 && maj < 90) ||
            (maj > 48 && maj < 57) ||
            maj == 63 ||
            maj == 46 ||
            maj == 47 ||
            maj == 37
        )
        {
            keyboard[5].GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1);
        } else 
        {
            keyboard[5].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }

        // loop throw keyboard to find the corresponding key
        foreach(GameObject item in keyboard)
        {
            // loop throw different rows of keys (AKeys, WKeys, ...)
            foreach(RectTransform children in item.GetComponentsInChildren<RectTransform>())
            {
                // exclude the name of the rows (AKeys, WKeys, ...) and maj
                switch(children.name)
                {
                    case "AKeys":
                        error = true; break;
                    case "QKeys":
                        error = true; break;
                    case "WKeys":
                        error = true; break;
                    case "NumberKeys":
                        error = true; break;
                    case "MAJ":
                        error = true; break;
                    default:
                        error = false; break;
                }

                // if the key isn't an excluded one
                if(!error)
                {
                    // find _nextCharacter in the name of the different keys
                    foreach(char a in children.name.ToCharArray())
                    {
                        // if all conditions required are forfilled than we change 
                        // the scale of the chosen key
                        if(_nextCharacter == a && !_ending)
                        {
                            children.localScale = new Vector3(2, 2, 1);
                            // we break to avoid reducing the size of the key to  (1, 1, 1) 
                            // if there is another character in the name of the key
                            break;
                        } else
                        {
                            children.localScale = new Vector3(1, 1, 1);
                        }
                    }
                }
            }
        }
    }

    void Awake()
    {
        text = GetComponent<TMP_Text>();
    }
    void Start()
    {
        HighLight(example[advancement]);
    }
    
    void Update()
    {
        // We test if the list of inputs isn't empty and if the user inputs a key
        if(Input.anyKeyDown && Input.inputString.Length >= 1)
        {
            // test if index is out of range
            if(advancement < example.Length - display.Length)
            {
                // if the user inputs the same character as the example
                if(Input.inputString[0] == (example[advancement]))
                {
                    advancement++;
                    // display : string --> char[]
                    display = FillDisplay(display.ToCharArray(), example[display.Length + advancement - 1]);
                    HighLight(example[advancement]);
                } 
            }
            // same : if index not out of range
            else if(advancement < example.Length - 1)
            {
                if(Input.inputString[0] == (example[advancement]))
                {
                    advancement++;
                    // display : string --> char[]
                    display = FillDisplay(display.ToCharArray(), ' ');
                    HighLight(example[advancement]);
                } 
            } else 
            // if the user has finished the example
            {
                if(Input.inputString[0] == (example[advancement]))
                {
                    display = FillDisplay(display.ToCharArray(), ' ');
                    HighLight(' ', true);
                }
            }
        }

        text.text = display;
        
    }
}
