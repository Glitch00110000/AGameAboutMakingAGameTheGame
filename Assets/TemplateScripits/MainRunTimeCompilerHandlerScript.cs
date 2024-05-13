using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoslynCSharp;
using Unity.VisualScripting;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class MainRunTimeCompilerHandlerScript : MonoBehaviour
{

    // runtime script stuff
    private ScriptDomain domain = null;

    public AssemblyReferenceAsset[] assemblyAssets = null;

    public ScriptProxy proxy = null;

    //UI stuff
    public TextMeshProUGUI errorSpace = null;
    // public TextMeshProUGUI tipSpace = null; // this might be acceset from diferent script
    public TMP_InputField InputField;

    public GameObject tipSpaceObject;
    public TextMeshProUGUI tipTextSpaceObj;

    [SerializeField]
    private string tipText;


    [SerializeField]
    private string CurrentSceneName;

    //private variavles
    private bool tipEnabler = true;

    // Start is called before the first frame update
    void Start()
    {
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
        }
        else
        {
            // instantiate the proxy (the script written by a player)
            proxy = type.CreateInstance(gameObject);
        }
    }

    public void DisposeOfProxy()
    {
        if (!proxy.IsDisposed)
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

}
