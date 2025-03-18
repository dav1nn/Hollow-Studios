using UnityEngine;
using TMPro;
using System.Collections;

public class ShakyTextEffect : MonoBehaviour
{
    public TMP_Text textMeshPro;
    public float shakeIntensity = 2f;
    public float shakeSpeed = 50f;
    public Color blinkColor = new Color(28f / 255f, 237f / 255f, 4f / 255f);

    private Vector3[] originalVertices;
    private TMP_TextInfo textInfo;
    private Color originalColor;
    private bool isBlinking = false;

    void Start()
    {
        if (textMeshPro == null)
            textMeshPro = GetComponent<TMP_Text>();

        originalColor = textMeshPro.color;
        StartCoroutine(BlinkEffect());
    }

    void Update()
    {
        if (textMeshPro == null) return;
        textMeshPro.ForceMeshUpdate();
        textInfo = textMeshPro.textInfo;

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            originalVertices = meshInfo.vertices;

            for (int j = 0; j < originalVertices.Length; j++)
            {
                Vector3 offset = new Vector3(
                    Mathf.Sin(Time.time * shakeSpeed + j) * shakeIntensity,
                    Mathf.Cos(Time.time * shakeSpeed + j) * shakeIntensity,
                    0);

                meshInfo.vertices[j] = originalVertices[j] + offset;
            }
            textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
        }
    }

    IEnumerator BlinkEffect()
    {
        while (true)
        {
            float waitTime = Random.Range(1f, 3f);
            yield return new WaitForSeconds(waitTime);

            textMeshPro.color = blinkColor;
            yield return new WaitForSeconds(0.1f);
            textMeshPro.color = originalColor;
        }
    }
}