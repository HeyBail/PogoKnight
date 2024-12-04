using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HammerController : MonoBehaviour
{
    private Animator animator;

    private float cooldownTimer = 0f;
    private float cooldownDuration = 1f;

    private float gemHealth = 4f;
    private float maxGemHealth = 4f;

    public GameObject gem;
    public GameObject crushingRoot;

    private bool hammerUsed = false;

    public Slider gemHealthBar;
    public Slider hammerCooldown;

    public HammerOscilator oscilator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void OnChop(InputValue value)
    {
        if (!hammerUsed)
        {
            animator.SetTrigger("hit");
            hammerUsed = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hammerUsed)
        {
            cooldownTimer += Time.deltaTime;
            hammerCooldown.value =  1 - (cooldownTimer / cooldownDuration);

            if (cooldownTimer >= cooldownDuration)
            {
                hammerUsed = false;
                cooldownTimer = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        gemHealth -= 1;
        gemHealthBar.value = gemHealth / maxGemHealth;
        if (gemHealth > 0)
        {
            oscilator.speed *= 1.3f;
        } else
        {
            oscilator.speed = 0;
            GameObject.Destroy(gem);
            StartCoroutine(EndAfterCooldown());
        }
    }

    IEnumerator EndAfterCooldown()
    {
        yield return new WaitForSeconds(1.5f);

        GameObject.Destroy(crushingRoot);

        // Wait for the next frame
        yield return null;
    }
}
