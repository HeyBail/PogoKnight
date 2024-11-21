using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    Transform playerLocation;

    [SerializeField]
    float interactDistance;

    [SerializeField]
    private Color color = Color.blue;

    [SerializeField]
    private string _sceneName;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, interactDistance);
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
        if (Vector3.Distance(transform.position, playerLocation.position) < interactDistance)
        {
            GameManager.Instance.setPlayerTransformData(FindFirstObjectByType<PlayerMovement>().gameObject.transform);
            SceneManager.LoadScene(_sceneName);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
