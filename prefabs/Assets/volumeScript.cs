using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeScript : MonoBehaviour
{
    // Material to modify
    public Material material;

    // Microphone input device
    public string microphone;

    // Audio source to capture microphone input
    private AudioSource audioSource;

    // Minimum and maximum fill amount for the material
    public float minFillAmount = 0.05f;
    public float maxFillAmount = 2f;

    // Threshold for changing the fill color
    public float volumeThreshold = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
        }
        else
        {
            Debug.LogError("Renderer component not found! Make sure the material is applied to the same GameObject as this script.");
        }

        // Check if there is at least one microphone
        if (Microphone.devices.Length > 0)
        {
            // Set the selected microphone
            microphone = Microphone.devices[0];

            // Initialize AudioSource component
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = Microphone.Start(microphone, true, 10, AudioSettings.outputSampleRate);
            audioSource.loop = true;

            // Wait until the microphone starts
            while (!(Microphone.GetPosition(null) > 0)) { }
            audioSource.Play();
        }
        else
        {
            Debug.LogError("No microphone found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (material == null)
        {
            Debug.LogError("Material is not assigned!");
            return;
        }

        // Calculate RMS volume level
        float[] samples = new float[256];
        audioSource.GetOutputData(samples, 0);
        float rmsValue = 0;
        foreach (float sample in samples)
        {
            rmsValue += sample * sample;
        }
        rmsValue /= samples.Length;
        rmsValue = Mathf.Sqrt(rmsValue);

        // Map RMS volume to fill amount scale
        float fillAmount = Mathf.Lerp(minFillAmount, maxFillAmount, rmsValue * 10);

        // Set fill amount in the material
        material.SetFloat("_Fill", fillAmount);

        // Check if volume exceeds the threshold
        if (fillAmount > volumeThreshold)
        {
            // Set the fill color to red
            material.SetColor("_FillColor", Color.red);
            Debug.Log("work");
        }
        else
        {
            // Reset the fill color (optional, if you want to change it back when volume is below the threshold)
            material.SetColor("_FillColor", Color.green); // Or any default color you prefer
        }
    }
}
