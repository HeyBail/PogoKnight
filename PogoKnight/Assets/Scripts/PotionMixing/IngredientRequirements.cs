using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngredientRequirements : MonoBehaviour
{
    public GameObject dicingMinigame;
    public GameObject requirementsHUD;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.PotionMakingReadyProp)
        {
            GameObject.Instantiate(dicingMinigame);
        } else
        {
            requirementsHUD.SetActive(true);
            StartCoroutine(ReturnToMainLevel());
        }
    }
    IEnumerator ReturnToMainLevel()
    {
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene("Level1");

        // Wait for the next frame
        yield return null;
    }
}
