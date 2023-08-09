using UnityEngine;
using UnityEngine.UI;

public class ButtonOpacity : MonoBehaviour
{
    [SerializeField]
    private float alpha = 0.5f;

    void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = alpha;
    }
}
