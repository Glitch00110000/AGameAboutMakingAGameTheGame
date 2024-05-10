using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialPaper : MonoBehaviour
{
    public GameObject TutorialImageObject;
    public TextMeshProUGUI TutorialTextSpaceObj;

    [SerializeField]
    private string TutorialText; 

    private bool WindowSwitch = true;

    // Start is called before the first frame update
    void Start()
    {
        TutorialTextSpaceObj.text = TutorialText;
    }

    public void OpenTutorialWindow()
    {
        if (WindowSwitch)
        {
            TutorialImageObject.SetActive(true);
            WindowSwitch = false;
        }
        else
        {
            TutorialImageObject.SetActive(false);
            WindowSwitch= true;
        }
    }

}