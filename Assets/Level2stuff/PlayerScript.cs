using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using RoslynCSharp;

public class PlayerScript : MonoBehaviour
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

    public GameObject tutPaperOpenButton;

    [SerializeField]
    private Animator animator;


    private DoorScript door;
    private bool canBeOpened = false;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        scriptProxy = scriptRunner.proxy;
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

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

        moveInput.Normalize();

        rb.velocity = moveInput * moveSpeed;

        if (scriptProxy != null)
        {
            if (canBeOpened)
            {
                scriptProxy.SafeCall("OpenDoor",door);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tutorial")
        {
            tutPaperOpenButton.SetActive(true);
        }
        if (collision.tag == "Door")
        {
            door = collision.GetComponent<DoorScript>();
            canBeOpened = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Tutorial")
        {
            tutPaperOpenButton.SetActive(false);
        }

        if (collision.tag == "Door")
        {
            door = null;
            canBeOpened = false;
        }
    }
}

