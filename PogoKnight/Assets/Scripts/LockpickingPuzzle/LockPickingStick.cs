using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPickingStick : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            MinigameManager_Lockpicking.Instance.LevelComplete();
        }
    }
}
