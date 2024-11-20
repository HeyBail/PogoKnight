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
    VerticalObstacle[] verticalObstacles;

    void Start()
    {
        miniGameCamera.gameObject.SetActive(true);
        verticalObstacles = FindObjectsOfType<VerticalObstacle>();
    }

    public static MinigameManager_Lockpicking Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LevelComplete() 
    { 
        switch (MinigameManager_Lockpicking.lockPickingState) //could make the blocks increase in speed, takes 3 hits to move to next one. could also switch direction
        {
            case LockPickingState.level1:
                Debug.Log("level 1 complete");
                increaseSpeed();
                lockPickingState = LockPickingState.level2;
                break;
            case LockPickingState.level2:
                Debug.Log("level 2 complete");
                increaseSpeed();
                lockPickingState = LockPickingState.level3;
                break;
            case LockPickingState.level3:
                winLevel();
                break;
        }
    }

    private void increaseSpeed() 
    {
        foreach (VerticalObstacle obstacle in verticalObstacles)
        {
            obstacle.SpeedProp *= 2;
        }
    }

    private void winLevel() 
    {
        GameManager.Instance.OpenChest();
        SceneManager.LoadScene("Level1");
    }
}
