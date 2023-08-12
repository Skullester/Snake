using UnityEngine;
using TMPro;
using YG;

public class GraphicsSetttings : MonoBehaviour
{
    private void Awake()
    {
        var dropdown = GetComponentInChildren<TMP_Dropdown>();
        dropdown.value = YandexGame.savesData.indexOfQuality;
        SetGraphics(dropdown.value);
        dropdown.RefreshShownValue();
    }

    public void SetGraphics(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
}
