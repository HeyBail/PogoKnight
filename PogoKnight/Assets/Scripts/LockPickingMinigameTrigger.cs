using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LockPickingMinigameTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject boxLidClosed;
    [SerializeField]
    GameObject boxLidOpen;
    [SerializeField]
    GameObject crystal;
    [SerializeField]
    GameObject indicator;

    [SerializeField]
    Transform playerLocation;

    public static LockPickingMinigameTrigger Instance;

    [SerializeField]
    float interactDistance;

    [SerializeField]
    private Color color = Color.blue;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, interactDistance);
    }

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
    private void Start()
    {
        playerLocation = FindFirstObjectByType<PlayerMovement>().transform;
        SetChestState(GameManager.Instance.ChestOpenProp);
    }
    void OnEnable()
    {
        PlayerMovement.onInteractChanged += Interact;
    }

    void OnDisable()
    {
        PlayerMovement.onInteractChanged -= Interact;
    }

    private void Interact()
    {
        if (!GameManager.Instance.ChestOpenProp && Vector3.Distance(transform.position, playerLocation.position) < interactDistance)
        {
            loadMiniGame();
        }
    }

    public void loadMiniGame() 
    {
        GameManager.Instance.setPlayerTransformData(FindFirstObjectByType<PlayerMovement>().gameObject.transform);
        SceneManager.LoadScene("LockPickingMinigame");
    }

    public void SetChestState(bool chestOpen) 
    {
        if (chestOpen)
        {
            boxLidClosed.SetActive(false);
            boxLidOpen.SetActive(true);
            indicator.SetActive(true);
            crystal.SetActive(true);
        }
        else
        {
            boxLidClosed.SetActive(true);
            boxLidOpen.SetActive(false);
            indicator.SetActive(false);
            crystal.SetActive(false);
        }
    }
}