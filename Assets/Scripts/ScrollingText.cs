using UnityEngine;
using TMPro;

public class ScrollingText : MonoBehaviour
{
    public Transform entryPoint;
    public Transform exitPoint;
    public float duration = 5f;
    
    public bool loop = true;
    
    public float constantAlpha = 0.8f;
    
    
    public string text1 = "Text One";
    public string text2 = "Text Two";
    public Color color1 = Color.green;
    public Color color2 = Color.red;
    public float flickerInterval = 0.5f; 
    public float colorAberrationAmount = 0.05f; 
    
    
    public float pulseAmplitude = 0.1f;  
    public float pulseFrequency = 2f;    

    
    private bool finishAfterCycle = false;
    
    private bool hasFinishedCycle = false;
    
    private float timer;
    private float flickerTimer;
    private bool flickerState;
    
    private TMP_Text textComponent;
    private Vector3 originalScale;
    
    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        if (entryPoint != null)
            transform.position = entryPoint.position;
        
        originalScale = transform.localScale;
        flickerTimer = 0f;
        flickerState = false;
        
        if (textComponent != null)
        {
            textComponent.text = text1;
            Color c = color1;
            c.a = constantAlpha;
            textComponent.color = c;
        }
    }
    
    void Update()
    {
        
        if (hasFinishedCycle)
            return;
        
        if (entryPoint == null || exitPoint == null)
            return;
        
        timer += Time.deltaTime;
        float t = timer / duration;
        
       
        if (t > 1f)
        {
            
            
            if ((loop && finishAfterCycle) || !loop)
            {
                t = 1f;
                hasFinishedCycle = true;
            }
            else if (loop)
            {
                timer = 0f;
                t = 0f;
                transform.position = entryPoint.position;
            }
        }
        
        transform.position = Vector3.Lerp(entryPoint.position, exitPoint.position, t);
        
        
        float noise = Mathf.PerlinNoise(Time.time * pulseFrequency, 0f) - 0.5f;
        float scaleFactor = 1f + noise * pulseAmplitude;
        transform.localScale = originalScale * scaleFactor;
        
        
        flickerTimer += Time.deltaTime;
        if (flickerTimer >= flickerInterval)
        {
            flickerState = !flickerState;
            flickerTimer = 0f;
        }
        
        if (textComponent != null)
        {
            Color baseColor = flickerState ? color2 : color1;
            textComponent.text = flickerState ? text2 : text1;
            
            
            Color finalColor = new Color(
                Mathf.Clamp(baseColor.r + Random.Range(-colorAberrationAmount, colorAberrationAmount), 0f, 1f),
                Mathf.Clamp(baseColor.g + Random.Range(-colorAberrationAmount, colorAberrationAmount), 0f, 1f),
                Mathf.Clamp(baseColor.b + Random.Range(-colorAberrationAmount, colorAberrationAmount), 0f, 1f),
                constantAlpha
            );
            textComponent.color = finalColor;
        }
    }
    
    
    
    public void FinishAfterCurrentCycle()
    {
        finishAfterCycle = true;
    }
}





