using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] Enemy enemy;

    public void FinishMovement()
    {
        enemy.FinishMovement();
    }
    public void EndCurrentState()
    {
        enemy.EndCurrentState();
    }
    public void StartAction()
    {
        enemy.StartAction();
    }
}
