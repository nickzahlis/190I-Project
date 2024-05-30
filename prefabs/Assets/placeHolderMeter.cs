using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterScript : MonoBehaviour
{
    // Material to modify
    public Material material;

    // Minimum and maximum fill amount for the material
    public float minFillAmount = 0.05f;
    public float maxFillAmount = 2f;

    // Speed at which the fill amount changes
    public float fillSpeed = 1f;

    // Direction of fill change (1 for increasing, -1 for decreasing)
    private int direction = 1;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (material == null)
        {
            Debug.LogError("Material is not assigned!");
            return;
        }

        // Get the current fill amount
        float currentFillAmount = material.GetFloat("_Fill");

        // Calculate the new fill amount
        float newFillAmount = currentFillAmount + direction * fillSpeed * Time.deltaTime;

        // Check if the fill amount needs to change direction
        if (newFillAmount >= maxFillAmount || newFillAmount <= minFillAmount)
        {
            direction *= -1;
            newFillAmount = Mathf.Clamp(newFillAmount, minFillAmount, maxFillAmount);
        }

        // Set the new fill amount in the material
        material.SetFloat("_Fill", newFillAmount);
    }
}
