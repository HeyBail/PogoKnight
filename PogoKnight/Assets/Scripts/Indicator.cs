using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    [SerializeField]
    private bool slimeIndicator;

    [SerializeField]
    private bool crystalIndicator;

    [SerializeField]
    private bool mushroomIndicator;

    [SerializeField]
    private bool defeatSlimeIndicator;

    [SerializeField]
    private bool openChestIndicator;

    [SerializeField]
    private bool potionMakingIndicator;

    [SerializeField]
    private float distance = .5f;
    [SerializeField]
    private float frequency = 1f;

    GameObject player;

    private Vector3 startPosition;

    void OnEnable()
    {
        GameManager.onGameProgressed += checkActive;
    }

    void OnDisable()
    {
        GameManager.onGameProgressed -= checkActive;
    }

    void Start()
    {
        startPosition = transform.position;
        player = FindFirstObjectByType<PlayerMovement>().gameObject;

        checkActive();
    }

    private void checkActive() 
    {
        bool setActive = false;

        if (slimeIndicator)
        {
            if (!GameManager.Instance.SlimeCollectedProp)
            {
                setActive = true;
            }
        }
        if (crystalIndicator)
        {
            if (!GameManager.Instance.CrystalCollectedProp)
            {
                setActive = true;
            }
        }
        if (mushroomIndicator)
        {
            if (!GameManager.Instance.MushroomCollectedProp)
            {
                setActive = true;
            }
        }
        if (defeatSlimeIndicator)
        {
            if (!GameManager.Instance.SlimeDefeatedProp)
            {
                setActive = true;
            }
        }
        if (openChestIndicator)
        {
            if (!GameManager.Instance.ChestOpenProp)
            {
                setActive = true;
            }
        }
        if (potionMakingIndicator)
        {
            if (GameManager.Instance.PotionMakingReadyProp)
            {
                setActive = true;
            }
        }

        gameObject.SetActive(setActive);
    }

    void Update()
    {
        float newHeight = startPosition.y + Mathf.Sin(Time.time * frequency) * distance;

        transform.position = new Vector3(startPosition.x, newHeight, startPosition.z);

        transform.LookAt(player.transform, Vector3.up);
    }
}
