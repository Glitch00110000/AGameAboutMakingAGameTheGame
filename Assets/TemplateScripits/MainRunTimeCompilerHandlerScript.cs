using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoslynCSharp;
using Unity.VisualScripting;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainRunTimeCompilerHandlerScript : MonoBehaviour
{

    // runtime script stuff
    private ScriptDomain domain = null;

    public AssemblyReferenceAsset[] assemblyAssets = null;

    public ScriptProxy proxy = null;

    private string SavedCode;

    //UI stuff
    public TextMeshProUGUI errorSpace = null;

    [SerializeField]
    private GameObject ErrorShower;
    // public TextMeshProUGUI tipSpace = null; // this might be acceset from diferent script
    public TMP_InputField InputField;

    public GameObject tipSpaceObject;
    public TextMeshProUGUI tipTextSpaceObj;

    public GameObject tutorialPanel;
    public GameObject closeButton;


    [SerializeField]
    private GameObject MenuScreen;

    [SerializeField]
    private Slider MusikSlider;

    [SerializeField]
    private AudioSource musik;

    [SerializeField]
    private AudioSource[] OtherAudio;


    [SerializeField]
    private Slider OtherSoundsSlider;


    [SerializeField]
    private string tipText;


    [SerializeField]
    private string CurrentSceneName;

    //private variavles
    private bool tipEnabler = true;

    // Start is called before the first frame update
    void Start()
    {
        musik.volume = PlayerPrefs.GetFloat("MusicVolume");
        MusikSlider.value = PlayerPrefs.GetFloat("MusicVolume");

        OtherSoundsSlider.value = PlayerPrefs.GetFloat("OtherSoundVolume");

        foreach (AudioSource a in OtherAudio)
        {            
            a.volume = PlayerPrefs.GetFloat("OtherSoundVolume");
        }
        MusikSlider.onValueChanged.AddListener((v) =>
        {
            musik.volume = v;
            PlayerPrefs.SetFloat("MusicVolume", v);
        });

        OtherSoundsSlider.onValueChanged.AddListener((v) =>
        {
            foreach (AudioSource a in OtherAudio)
            {
                a.volume = v;
                PlayerPrefs.SetFloat("OtherSoundVolume", v);
            }
        });





        SavedCode = InputField.text;
        // should the compiler be inicialized
        bool initCompiler = true;

        //Creates domain for our runtime scripts
        domain = ScriptDomain.CreateDomain("TestDomain", initCompiler);
        foreach (AssemblyReferenceAsset asset in assemblyAssets)
        {
            domain.RoslynCompilerService.ReferenceAssemblies.Add(asset);
        }

        //sets the tip for current level
        tipTextSpaceObj.text = tipText; 
    }

    public void CompileScript()
    {
        errorSpace.text = "error space";
        string source = InputField.text;
        ScriptType type = domain.CompileAndLoadMainSource(source);

        //error space stuff
        if (type == null)
        {
            if (domain.RoslynCompilerService.LastCompileResult.Success == false)
                errorSpace.text = "Your code contained errors. Please fix and try again";
            else if (domain.SecurityResult.IsSecurityVerified == false)
                errorSpace.text = "Your code failed code security verification";
            else
                errorSpace.text = "Your code does not define a class. You must include one class definition'";
            ErrorShower.SetActive(true);
        }
        else
        {
            // instantiate the proxy (the script written by a player)
            ErrorShower.SetActive(false);
            proxy = type.CreateInstance(gameObject);
        }
    }

    public void DisposeOfProxy()
    {
        ErrorShower.SetActive(false);
        errorSpace.text = "error space";
        if (!proxy.IsDisposed && proxy != null)
        {
            proxy.Dispose();
        }
    }


    public void EnableTipSpace()
    {
        if (tipEnabler)
        {
            tipSpaceObject.SetActive(true);
            tipEnabler = false;
        }
        else
        {
            tipSpaceObject.SetActive(false);
            tipEnabler = true;
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(CurrentSceneName);
    }

    public void OpenTutPaperTextPanel()
    {
        tutorialPanel.SetActive(true);
        closeButton.SetActive(true);
    }

    public void Close()
    {
        tutorialPanel.SetActive(false);
        closeButton.SetActive(false);
    }

    public void SaveCode()
    {
        SavedCode = InputField.text;
    }

    public void LoadCode()
    {
        InputField.text = SavedCode;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenMenu()
    {
        MenuScreen.SetActive(true);
    }

    public void CloseMenu()
    {
        MenuScreen.SetActive(false);
    }

}
