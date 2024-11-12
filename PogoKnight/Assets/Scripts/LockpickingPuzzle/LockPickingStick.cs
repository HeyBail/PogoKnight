using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPickingStick : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("your mother");
        if (collision.gameObject.tag == "Goal")
        {
            Debug.Log("You Win");
            // win or next level
            //collision.gameObject.GetComponent<Goal>().complete;
        }
    }
}
