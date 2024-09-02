using UnityEngine;
using TMPro;
public class FloatingText : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float fadeSpeed = 1f;
    private TextMeshProUGUI textMesh;
    private Color originalColor;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        originalColor = textMesh.color;
    }

    void Update()
    {
        // Move the text upward
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        // Fade out the text
        textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, textMesh.color.a - (fadeSpeed * Time.deltaTime));

        // Destroy the text after it fades out
        if (textMesh.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}