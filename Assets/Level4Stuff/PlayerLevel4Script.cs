using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using RoslynCSharp;

public class PlayerLevel4Script : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float moveSpeed;
    private Vector2 moveInput;


    [SerializeField]
    private GameObject[] sheep;

    [SerializeField]
    private ScriptProxy scriptProxy;
    [SerializeField]
    private MainRunTimeCompilerHandlerScript scriptRunner;


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
            scriptProxy.SafeCall("ElectricSheep");
            bool old = (bool)scriptProxy.Fields["electricSheepAre"];
            if (!old)
            {
                ChangeSpheep();
            }

        }
    }

    private void ChangeSpheep()
    {
        foreach (GameObject sheeppl in sheep)
        {
            sheeppl.SetActive(false);
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

