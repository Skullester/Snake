using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadScreen : MonoBehaviour
{
    private Slider slider;
    private float offset = 0.35f;

    [SerializeField]
    private GameObject loadScreenObj;
    private static bool isGameStarted;

    private void Awake()
    {
        if (isGameStarted)
        {
            loadScreenObj.SetActive(false);
            return;
        }
        slider = GetComponentInParent<Slider>();
        StartCoroutine(SubstactSliderValue());
    }

    IEnumerator SubstactSliderValue()
    {
        while (slider.value < 1)
        {
            slider.value += Time.deltaTime * offset;
            yield return null;
        }
        yield return new WaitForSeconds(0.6f);
        loadScreenObj.GetComponent<Animator>().SetTrigger("StartGame");
        yield return new WaitForSeconds(1.5f);
        isGameStarted = true;
        loadScreenObj.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.SetActive(false);
    }
}
