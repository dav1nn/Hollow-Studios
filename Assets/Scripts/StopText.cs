using UnityEngine;

public class StopText : MonoBehaviour
{
    public ScrollingText[] scrollingTexts;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (ScrollingText st in scrollingTexts)
            {
                if (st != null)
                {
                    st.FinishAfterCurrentCycle();
                }
            }
        }
    }
}



