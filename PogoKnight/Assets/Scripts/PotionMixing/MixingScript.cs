using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixingScript : MonoBehaviour
{

    public Animator stirAnimator;
    public GameObject endScreen;
    public GameObject potion;
    public ParticleSystem particles;

    private Animator potionAnimator;
    // Start is called before the first frame update
    void Start()
    {
        potionAnimator = potion.GetComponent<Animator>();
        StartCoroutine(StartParticles());
        StartCoroutine(RisePotion());
        StartCoroutine(ShowEndScreen());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartParticles()
    {
        yield return new WaitForSeconds(1f);

        particles.Play();

        yield return null;
    }

    IEnumerator RisePotion()
    {
        yield return new WaitForSeconds(2.5f);

        potionAnimator.SetTrigger("rise");

        yield return null;
    }

    IEnumerator ShowEndScreen()
    {
        yield return new WaitForSeconds(4f);

        stirAnimator.SetTrigger("idle");
        endScreen.SetActive(true);
        // Wait for the next frame
        yield return null;
    }
}
