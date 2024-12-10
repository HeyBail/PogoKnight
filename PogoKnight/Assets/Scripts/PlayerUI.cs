using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    GameObject crystalUI;

    [SerializeField]
    GameObject slimeUI;

    [SerializeField]
    GameObject mushroomUI;

    public static PlayerUI Instance;
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

    // Start is called before the first frame update
    void Start()
    {
        CheckCollectibles();
    }

    void OnEnable()
    {
        GameManager.onGameProgressed += CheckCollectibles;
    }

    void OnDisable()
    {
        GameManager.onGameProgressed -= CheckCollectibles;
    }

    private void CheckCollectibles() 
    {
        if (GameManager.Instance.CrystalCollectedProp) 
        {
            crystalUI.SetActive(true);
        }
        if (GameManager.Instance.SlimeCollectedProp)
        {
            slimeUI.SetActive(true);
        }
        if (GameManager.Instance.MushroomCollectedProp)
        {
            mushroomUI.SetActive(true);
        }
    }

    public void updateSlider(float normalizedValue) 
    {
        slider.value = Mathf.Clamp(normalizedValue, 0, 1);
    }
}
