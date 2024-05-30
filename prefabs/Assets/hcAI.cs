using System.Collections;
using TMPro;
using UnityEngine;

public class FlashingInstructions : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    public float instructionDisplayTime = 5.0f; // Time each instruction is displayed
    public float instructionInterval = 3.0f; // Time between instructions

    // List of instructions
    private string[] instructions = {
        "Reposition in front of the microphone",
        "Lower your speaking volume",
        "Speak slower"
    };

    private void Start()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshPro>();
        }

        StartCoroutine(FlashInstructions());
    }

    private IEnumerator FlashInstructions()
    {
        while (true)
        {
            // Display an instruction
            textMeshPro.text = GetRandomInstruction();
            textMeshPro.enabled = true;

            // Wait for the display time
            yield return new WaitForSeconds(instructionDisplayTime);

            // Hide the instruction
            textMeshPro.enabled = false;

            // Wait for the interval time before displaying the next instruction
            yield return new WaitForSeconds(instructionInterval);
        }
    }

    private string GetRandomInstruction()
    {
        int index = Random.Range(0, instructions.Length);
        return instructions[index];
    }
}