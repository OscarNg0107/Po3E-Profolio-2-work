using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashIDs : MonoBehaviour
{
    public int dyingState;
    public int deadbool;
    public int walkState;
    public int waveState;
    public int speedFloat;
    public int sneakingBool;
    public int wavingBool;
    public int walkBackwardBool;
    public int runningBool;
    public int jumpingBool;
    public int walkLeftBool;
    public int walkRightBool;

    private void Awake()
    {
        dyingState = Animator.StringToHash("BaseLayer.Dying");
        deadbool = Animator.StringToHash("Dead");
        walkState = Animator.StringToHash("BaseLayer.Walk_front");
        waveState = Animator.StringToHash("Waving.Wave");
        speedFloat = Animator.StringToHash("Speed");
        sneakingBool = Animator.StringToHash("Sneaking");
        wavingBool = Animator.StringToHash("Waving");
        walkBackwardBool = Animator.StringToHash("Walk Backwards");
        runningBool = Animator.StringToHash("Run");
        jumpingBool = Animator.StringToHash("Jump");
    }
}
