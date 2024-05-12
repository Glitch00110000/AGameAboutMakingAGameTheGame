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


    private Collider2D door;
    private bool canBeOpened = false;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        scriptProxy = scriptRunner.proxy;
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        rb.velocity = moveInput * moveSpeed;

        if (scriptProxy != null)
        {
            if (canBeOpened)
            {
                scriptProxy.SafeCall("OpedDoor",door,canBeOpened);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tutorial")
        {
            Debug.Log("tut");
        }
        if (collision.tag == "Door")
        {
            door = collision;
            canBeOpened = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Door")
        {
            door = null;
            canBeOpened = false;
        }
    }
}

