using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AxeController : MonoBehaviour
{

    public GameObject choppingParticles;
    public GameObject mushroom;
    public GameObject dicedMushroom;
    public GameObject choppingRoot;
    public GameObject next;

    private ParticleSystem particles;

    private bool isChopping = false;
    private float cooldownTimer = 0f;
    private float cooldownDuration = 2f;

    private Animator animator;
    private Animator rootAnimator;
    private float numCuts = 0f;
    private float maxCuts = 25;

    private float requiredCuts = 3f; // cuts required to start particles
    private float cutCounter = 0f; // current 'start up' cuts

    public Slider progressBar;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rootAnimator = choppingRoot.GetComponent<Animator>();
        particles = choppingParticles.GetComponent<ParticleSystem>();
        particles.Stop();

        if (progressBar != null)
        {
            progressBar.minValue = 0f; // Minimum value
            progressBar.maxValue = maxCuts; // Maximum value (or any desired value)
            progressBar.value = 0f; // Initial progress
        }
    }

    void OnChop(InputValue value)
    {
        if (numCuts < maxCuts)
        {
            isChopping = true;
            cooldownTimer = 0f;

            animator.SetTrigger("chop");

            numCuts += 1f;
            cutCounter += 1f;

            Debug.Log(numCuts);

            if (cutCounter == requiredCuts)
            {
                particles.Play();
            }

            if (progressBar != null)
            {
                progressBar.value = Mathf.Clamp(numCuts, progressBar.minValue, progressBar.maxValue);
            }

            if (numCuts == maxCuts)
            {
                particles.Stop();
                if (mushroom != null)
                {
                    GameObject.Destroy(mushroom);
                }
                GameObject.Instantiate(dicedMushroom);
               
            }
        } else
        {
            animator.SetTrigger("swipe");
            rootAnimator.SetTrigger("dump");
            StartCoroutine(EndAfterCooldown());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isChopping && numCuts < maxCuts)
        {
            cooldownTimer += Time.deltaTime;

            if (cooldownTimer >= cooldownDuration)
            {
                cutCounter = 0f;
                particles.Stop();
                isChopping = false;
                cooldownTimer = 0f;

                if(numCuts < 5f)
                {
                    numCuts = 0f;
                } else
                {
                    numCuts -= 5f;
                }

                if (progressBar != null)
                {
                    progressBar.value = Mathf.Clamp(numCuts, progressBar.minValue, progressBar.maxValue);
                }
            }
        }
    }

    IEnumerator EndAfterCooldown()
    {
        yield return new WaitForSeconds(2.5f);
        GameObject.Instantiate(next);
        GameObject.Destroy(choppingRoot);

        // Wait for the next frame
        yield return null;
    }
}
