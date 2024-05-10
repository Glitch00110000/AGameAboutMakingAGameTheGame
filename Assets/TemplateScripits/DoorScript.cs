using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField]
    private Collider2D selfCollider;
    [SerializeField]
    private SpriteRenderer selfRenderer;

    [SerializeField]
    private Sprite OpenedDoorTile;
    [SerializeField]
    private Sprite ClosedDoorTile;

    private bool DoorSwitch = true;


    public void OpenOrCloseDoor()
    {
        if (DoorSwitch)
        {
            selfRenderer.sprite = OpenedDoorTile;
            DoorSwitch = false;
            selfCollider.enabled = false;
        }
        else
        {
            selfRenderer.sprite = ClosedDoorTile;
            DoorSwitch = true;
            selfCollider.enabled = true;
        }
    }

}
