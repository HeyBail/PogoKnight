using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class ShakeController : MonoBehaviour
{
    [Header("Shake Settings")]
    public float shakeThreshold = 15f; // Threshold for shake detection
    public float shakeDuration = 5f;  // Time required to shake the jar
    public Transform jarObject;      // The jar to visually shake
    public float shakeIntensity = 5f; // Intensity of the visual shake

    private Vector3 initialJarPosition;
    private float shakeTime = 0f;
    private bool isShaking = false;
    private bool complete = false;

    public Slider shakingProgress;
    public ParticleSystem particles;

    public Animator animator;
    private CameraController camera;

    public GameObject shakeRoot;
    public GameObject next;

    private void Start()
    {
        if (jarObject != null)
        {
            initialJarPosition = jarObject.localPosition;
        }

        jarObject = gameObject.transform;
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }

    private void Update()
    {
        // If shaking is active, increment shakeTime
        if (isShaking && !complete)
        {
            shakeTime += Time.deltaTime;

            shakingProgress.value = shakeTime / shakeDuration;

            // If the player shakes the jar long enough, trigger success
            if (shakeTime >= shakeDuration)
            {
                Debug.Log("Shaking Complete!");
                isShaking = false;
                complete = true;
                animator.SetTrigger("yeet");
                OnShakeComplete();
            }

            // Visual shake effect
            if (jarObject != null)
            {
                jarObject.localPosition = initialJarPosition + (Vector3)(Random.insideUnitCircle * shakeIntensity);
            }
        }
        else
        {
            // Gradually reset the jar position when not shaking
            if (jarObject != null)
            {
                jarObject.localPosition = Vector3.Lerp(jarObject.localPosition, initialJarPosition, Time.deltaTime * 10f);
            }
        }
    }

    void OnShake(InputValue value)
    {
        Vector2 shakeInput = value.Get<Vector2>();

        // Check if the shake input exceeds the threshold
        if (shakeInput.magnitude > shakeThreshold && !complete)
        {
            isShaking = true;
            particles.Play();
        }
        else
        {
            isShaking = false;
            particles.Stop();
        }
    }

    private void OnShakeComplete()
    {
        // Reset the jar's position and perform any logic when shaking is done
        if (jarObject != null)
        {
            jarObject.localPosition = initialJarPosition;
        }

        // Add your logic for completing the shaking stage
        StartCoroutine(EndAfterCooldown());
    }

    IEnumerator EndAfterCooldown()
    {
        yield return new WaitForSeconds(1.5f);

        GameObject.Destroy(shakeRoot);
        camera.MoveToTarget();
        GameObject.Instantiate(next);

        // Wait for the next frame
        yield return null;
    }

}
