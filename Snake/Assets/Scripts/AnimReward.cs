using UnityEngine;
using TMPro;

public class AnimReward : MonoBehaviour
{
    [SerializeField, Header("Экземпляр \"Counter\"")]
    private Counter counterInstance;
    private float multiplier = 1.3f;

    private void SetRewardCounter()
    {
        gameObject.SetActive(false);
        counterInstance.SetCounters(multiplier);
    }
}
