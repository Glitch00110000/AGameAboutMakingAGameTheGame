using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using RoslynCSharp;
using UnityEngine.UI;

public class PlayerScriptLv1 : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float moveSpeed;
    private Vector2 moveInput;

    [SerializeField]
    private ScriptProxy scriptProxy;
    [SerializeField]
    private MainRunTimeCompilerHandlerScript scriptRunner;

    [SerializeField]
    private TutorialPaper tutPaper;
    [SerializeField]
    public GameObject tutPaperOpenButton;

    [SerializeField]
    private Animator animator;


    // Start is called before the first frame update
    private void Start()
    {
        scriptRunner.CompileScript();
    }
    // Update is called once per frame
    void Update()
    {
        scriptProxy = scriptRunner.proxy;

        if (scriptProxy != null)
        {
            scriptProxy.SafeCall("PlayerMovement", gameObject, moveSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            animator.Play("moveRight");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            animator.Play("moveLeft");
        }
        else if (Input.GetKey(KeyCode.W))
        {
            animator.Play("moveBack");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            animator.Play("moveFront");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tutorial")
        {
            tutPaperOpenButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Tutorial")
        {
            tutPaperOpenButton.SetActive(false);
        }
    }
}

