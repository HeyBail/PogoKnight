using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField]
    private bool slimeCollectible;

    [SerializeField]
    private bool crystalCollectible;

    [SerializeField]
    private bool mushroomCollectible;

    [SerializeField]
    private float rotationSpeed = 50f;

    private void Start()
    {
        if (slimeCollectible)
        {
            if (GameManager.Instance.SlimeCollectedProp)
            {
                gameObject.SetActive(false);
            }
        }
        else if (crystalCollectible)
        {
            if (GameManager.Instance.CrystalCollectedProp)
            {
                gameObject.SetActive(false);
            }
        }
        else if (mushroomCollectible)
        {
            if (GameManager.Instance.MushroomCollectedProp)
            {
                gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void collectItem() 
    {
        if (slimeCollectible)
        {
            GameManager.Instance.CollectSlime();
        }
        else if (crystalCollectible)
        {
            GameManager.Instance.CollectCrystal();
        }
        else if (mushroomCollectible)
        {
            GameManager.Instance.CollectMushroom();
        }

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            collectItem();
        }
    }
}
