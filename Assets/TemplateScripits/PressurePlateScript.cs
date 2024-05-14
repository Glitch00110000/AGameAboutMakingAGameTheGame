using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{
    [SerializeField]
    private DoorScript door;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite unPushed;
    [SerializeField]
    private Sprite Pushed;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Box")
        {
            door.OpenOrCloseDoor();
            spriteRenderer.sprite = Pushed;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Box")
        {
             door.OpenOrCloseDoor();
             spriteRenderer.sprite = unPushed;
        }
    }

}
