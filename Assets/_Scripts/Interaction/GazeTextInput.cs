using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class GazeTextInput : MonoBehaviour
{
    // public Text studentName;        // drag your text object on here
    public InputField inputField;
    
    private bool shiftOn = false;

    public void ClickLetter(string letterClicked)
    {
        if(shiftOn) {
            letterClicked = letterClicked.ToUpper();
        }

        string modifiedString = inputField.text + letterClicked;
        inputField.text = modifiedString;
    }

    public void ClickBackspace()
    {
        string currentName = inputField.text;
        if(currentName.Length > 0)
        {
            string tempString = currentName.Substring(0, currentName.Length -1);
            inputField.text = tempString;
        }
    }

    public void PressShift()
    {
        shiftOn = !shiftOn;
    }
}
