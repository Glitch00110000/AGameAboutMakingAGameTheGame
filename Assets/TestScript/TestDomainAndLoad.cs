using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoslynCSharp;

public class TestDomainAndLoad : MonoBehaviour
{
    private ScriptDomain domain = null;

    private string source =
        "using UnityEngine;" +
        "class Test : MonoBehaviour" +
        "{" +
        " void SayHello()" +
        " {" +
        " Debug.Log(\"Hello World\");" +
        " }" +
        "}";


    // Called by Unity
    void Start()
    {
        // Should we initialize the compiler?
        bool initCompiler = true;

        // Create the domain
        domain = ScriptDomain.CreateDomain("MyDomain",
       initCompiler);

        ScriptType type = domain.CompileAndLoadMainSource(source);

        ScriptProxy proxy = type.CreateInstance(gameObject);

        proxy.Call("SayHello");
    }


}
