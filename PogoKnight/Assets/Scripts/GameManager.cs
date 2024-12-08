using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //used to store the last transform of the player when leaving the scene
    public Dictionary<string, TransformData?> levelPlayerTransformData = new Dictionary<string, TransformData?>();

    public string lastLevelName;

    private bool _chestOpen;
    public bool ChestOpenProp
    {
        get { return _chestOpen; }
    }

    private bool _slimeDefeated;
    public bool SlimeDefeatedProp
    {
        get { return _slimeDefeated; }
    }

    private bool _slimeCollected;
    public bool SlimeCollectedProp
    {
        get { return _slimeCollected; }
    }

    private bool _crystalCollected;
    public bool CrystalCollectedProp
    {
        get { return _crystalCollected; }
    }

    private bool _mushroomCollected;
    public bool MushroomCollectedProp
    {
        get { return _mushroomCollected; }
    }

    public delegate void OnCollectItem();
    public static event OnCollectItem onCollectItem;

    public static GameManager Instance;
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

        // fills dictionary with scene names as keys and null transforms
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            levelPlayerTransformData.Add(sceneName, null);
        }
    }

    public TransformData? getPlayerTransformData()
    {
        return levelPlayerTransformData[SceneManager.GetActiveScene().name];
    }

    public void setPlayerTransformData(TransformData transformData)
    {
        levelPlayerTransformData[SceneManager.GetActiveScene().name] = transformData;
    }

    public void setPlayerTransformData(Transform transform)
    {
        levelPlayerTransformData[SceneManager.GetActiveScene().name] = new TransformData(transform);
    }

    public bool OpenChest() 
    {
        return _chestOpen = true;
    }

    public bool DefeatSlime()
    {
        return _slimeDefeated = true;
    }

    public bool CollectSlime()
    {
        onCollectItem?.Invoke();
        return _slimeCollected = true;
    }

    public bool CollectCrystal()
    {
        onCollectItem?.Invoke();
        return _crystalCollected = true;
    }

    public bool CollectMushroom()
    {
        onCollectItem?.Invoke();
        return _mushroomCollected = true;
    }
}
