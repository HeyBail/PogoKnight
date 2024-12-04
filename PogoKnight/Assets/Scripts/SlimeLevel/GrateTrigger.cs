using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrateTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Slime")
        {
            MinigameManager_Slime.Instance.SlimeGrated();
        }
    }
}
