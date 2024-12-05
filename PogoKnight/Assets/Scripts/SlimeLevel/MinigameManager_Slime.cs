using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager_Slime : MonoBehaviour
{
    [SerializeField]
    private GameObject slime;

    [SerializeField]
    private GameObject slimeJarEmpty;

    [SerializeField]
    private GameObject slimeJarFull;

    void Start()
    {

    }

    public static MinigameManager_Slime Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SlimeGrated() 
    {
        slime.SetActive(false);
        slimeJarEmpty.SetActive(false);
        slimeJarFull.SetActive(true);
    }
}
