using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyMachineScript : MonoBehaviour
{
    private Animator animator;
    private string currentAnimation = "";
    [SerializeField]
    private bool isAssembler;
    [SerializeField]
    private GameObject player;
    private float timer = 2.16f;

    private bool doneAssembling = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (isAssembler)
        {
            ChangeAnimation("AssemblyMachineAnimationAssembling");
        }
        else
        {
            ChangeAnimation("AssemblyMachineAnimationIdle");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isAssembler && !doneAssembling)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                player.SetActive(true);
                ChangeAnimation("AssemblyMachineAnimationIdle");
                doneAssembling = true;
            }
        }

    }


    private void ChangeAnimation(string animation)
    {
        if (currentAnimation != animation)
        {
            currentAnimation = animation;
            animator.Play(animation);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAssembler && collision.tag == "Player")
        {
            player.SetActive(false);
            ChangeAnimation("AssemblyMachineAnimationDissassembling");
        }
    }
}
