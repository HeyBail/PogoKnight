using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager_Lockpicking : MonoBehaviour
{
    [SerializeField]
    static LockPickingState lockPickingState = LockPickingState.level1;
    
    [SerializeField]
    Camera miniGameCamera;

    void Start()
    {
        miniGameCamera.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void LevelComplete() 
    { 
        switch (MinigameManager_Lockpicking.lockPickingState) //could make the blocks increase in speed, takes 3 hits to move to next one. could also switch direction
        {
            case LockPickingState.level1:
                Debug.Log("level 1 complete");
                lockPickingState = LockPickingState.level2;
                break;
            case LockPickingState.level2:
                Debug.Log("level 2 complete");
                lockPickingState = LockPickingState.level3;
                break;
            case LockPickingState.level3:
                winLevel();
                break;
        }
    }

    private static void winLevel() 
    {
        Debug.Log("you win");
        SceneManager.LoadScene("Level1");
    }
}
