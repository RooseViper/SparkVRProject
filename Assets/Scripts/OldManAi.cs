using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Pathfinding;
using UnityEngine;

public class OldManAi : MonoBehaviour
{
    [SerializeField]private BehaviorTree chaseBehavior;
    [SerializeField]private AudioSource chaseAudioSource;
    private Animator animator;

    private static readonly int IsWalking = Animator.StringToHash("IsWalking");

    // Start is called before the first frame update
    private void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    public void LockDown()
    {
        animator.SetBool(IsWalking, true);
        chaseAudioSource.Play();
    }

    public void Chase()
    {
        chaseBehavior.EnableBehavior();
    }
}
