using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using RoslynCSharp;

public class PlayerScriptLvl3 : MonoBehaviour
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
    private Rigidbody2D box;

    [SerializeField]
    public GameObject tutPaperOpenButton;

    [SerializeField]
    private Animator animator;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        scriptProxy = scriptRunner.proxy;
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        rb.velocity = moveInput * moveSpeed;

        if (moveInput.x > 0)
        {
            animator.Play("moveRight");
        }
        else if (moveInput.x < 0)
        {
            animator.Play("moveLeft");
        }
        else if (moveInput.y > 0)
        {
            animator.Play("moveBack");
        }
        else if (moveInput.y < 0)
        {
            animator.Play("moveFront");
        }

        if (scriptProxy != null)
        {
            scriptProxy.SafeCall("MovableBox",box);
            
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

