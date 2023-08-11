using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadScreen : MonoBehaviour
{
    [SerializeField]
    private Image[] imagesItems;

    [SerializeField]
    private Sprite[] spritesItems;

    [SerializeField]
    private Image logoImg;

    [SerializeField]
    private Image fillImg;
    private Image loadScreenImg;
    private Image snakeImg;

    [SerializeField]
    private Sprite[] spritesScreen;

    [SerializeField]
    private Sprite[] spritesLogoSnake;

    [SerializeField]
    private Sprite[] spriteSnakeSlider;
    private Slider slider;
    private float offset = 0.35f;

    [SerializeField]
    private GameObject loadScreenObj;
    public static bool IsGameStarted;

    private void Awake()
    {
        if (IsGameStarted)
        {
            loadScreenObj.SetActive(false);
            return;
        }
        snakeImg = GetComponent<Image>();
        loadScreenImg = loadScreenObj.GetComponent<Image>();
        slider = GetComponentInParent<Slider>();
        StartCoroutine(SubstactSliderValue());
    }

    void Start()
    {
        RollRandomSnake();
    }

    private void RollRandomSnake()
    {
        int index = Random.Range(0, ThemeChanger.ThemeCount);
        loadScreenImg.sprite = spritesScreen[index];
        snakeImg.sprite = spriteSnakeSlider[index];
        fillImg.color = ThemeChanger.Colors[index];
        logoImg.sprite = spritesLogoSnake[index];
        Sprite sprite = spritesItems[index];
        for (int i = 0; i < imagesItems.Length; i++)
            imagesItems[i].sprite = sprite;
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
        yield return new WaitForSeconds(1f);
        IsGameStarted = true;
        loadScreenObj.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.SetActive(false);
    }
}
