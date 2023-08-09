using UnityEngine;
using UnityEngine.EventSystems;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 scale;
    private ThemeChanger instanceThemeChanger;

    void Start()
    {
        instanceThemeChanger = GetComponentInParent<ThemeChanger>(true);
        scale = gameObject.transform.localScale;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        instanceThemeChanger.PlaySound(2);
        gameObject.transform.localScale = scale * 1.15f;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        gameObject.transform.localScale = scale;
    }
}
