using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggerHashIDs : MonoBehaviour
{
    public int speedFloat;
    public int backwardBool;
    public int turnLeftBool;
    public int turnRightBool;
    public int diggingBool;

    private void Awake()
    {
        speedFloat = Animator.StringToHash("Speed");
        backwardBool = Animator.StringToHash("Backward");
        turnLeftBool = Animator.StringToHash("TurnLeft");
        turnRightBool = Animator.StringToHash("TurnRight");
        diggingBool = Animator.StringToHash("Digging");
    }
}
