using UnityEngine;

public class AnimReward : MonoBehaviour
{
    [SerializeField, Header("Экземпляр \"Counter\"")]
    private Counter counterInstance;
    private float multiplier = 1.2f;

    private void SetRewardCounter()
    {
        gameObject.SetActive(false);
        counterInstance.SetCounters(multiplier);
    }
}
