using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField]
    private Collider2D selfColliderClosed;

    [SerializeField]
    private Collider2D selfColliderOpened1;
    [SerializeField]
    private Collider2D selfColliderOpened2;

    [SerializeField]
    private bool OpenTheDoorOnStart;


    [SerializeField]
    private SpriteRenderer selfRenderer;

    [SerializeField]
    private Sprite OpenedDoorTile;
    [SerializeField]
    private Sprite ClosedDoorTile;

    private bool DoorSwitch = true;


    private void Start()
    {
        if (OpenTheDoorOnStart)
        {
            OpenOrCloseDoor();
        }
    }


    public void OpenOrCloseDoor()
    {
        if (DoorSwitch)
        {
            selfRenderer.sprite = OpenedDoorTile;
            DoorSwitch = false;
            selfColliderClosed.enabled = false;
            selfColliderOpened1.enabled = true;
            selfColliderOpened2.enabled = true;
        }
        else
        {
            selfRenderer.sprite = ClosedDoorTile;
            DoorSwitch = true;
            selfColliderClosed.enabled = true;
            selfColliderOpened1.enabled = false;
            selfColliderOpened2.enabled = false;
        }
    }

}
