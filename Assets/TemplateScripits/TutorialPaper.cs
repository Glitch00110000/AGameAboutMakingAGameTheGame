using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialPaper : MonoBehaviour
{
    
    public TextMeshProUGUI TutorialTextObj;

    [SerializeField]
    private string TutorialText; 


    // Start is called before the first frame update
    void Start()
    {
        TutorialTextObj.text = TutorialText;
    }


}
