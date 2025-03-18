using UnityEngine;
using TMPro;

public class ShakyTextEffect : MonoBehaviour
{
    public TMP_Text textMeshPro;
    public float shakeIntensity = 2f;
    public float shakeSpeed = 50f;

    private Vector3[] originalVertices;
    private TMP_TextInfo textInfo;

    void Start()
    {
        if (textMeshPro == null)
            textMeshPro = GetComponent<TMP_Text>();
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
}
