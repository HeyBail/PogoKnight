using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager_Lockpicking : MonoBehaviour
{
    [SerializeField]
    Camera miniGameCamera;
    // Start is called before the first frame update
    void Start()
    {
        miniGameCamera.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
