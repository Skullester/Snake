using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;
using System;
using System.Collections;

public class Theme : MonoBehaviour
{
    [SerializeField, Header("Аниматор спрайт счетчика")]
    private Animator animatorSpriteCounter;

    [SerializeField, Header("Text multiplier")]
    private TMP_Text Multiplier;

    [SerializeField, Header("Спрайт предмета в Tasks"), FormerlySerializedAs("counterImgInTasks")]
    private Image victoryImgCounter;

    [SerializeField, Header("Спрайт предмета в Tasks")]
    private Image imgTask;

    [SerializeField, Header("UnlockImg")]
    private Image unlockImg;

    [SerializeField, Header("Текст названия темы"), FormerlySerializedAs("themeNameText")]
    private TMP_Text themeNameTextInShop;

    [SerializeField, Header("Кнопка Gamemode")]
    private Image btnPlaymode;

    [SerializeField, Header("Объект лока кнопки")]
    private GameObject blockBtnObj;
    private Button blockBtn;
    private int index;
    public static int IndexOfSceneBeforeShop;

    [Header("Магазин")]
    [SerializeField, Header("Текст цены")]
    private TMP_Text priceText;

    [SerializeField, Header("Текст кнопки \"Разблокировать\"")]
    private TMP_Text unlockText;
    private Button unlockBtn;
    private float price;

    [Space(10f)]
    [SerializeField, Header("Кнопка магазина")]
    private Image shopBtn;

    [SerializeField, Header("Панелька для кнопки рекламы")]
    private Image panelForAD;

    [SerializeField, Header("Панелька для кнопки рекламы в Tasks")]
    private Image panelForADInTasks;

    [SerializeField, Header("Кнопка \" меню\"")]
    private Image menuBtn;

    [SerializeField, Header("Кнопка \"Продолжить\"")]
    private Image continueImg;

    [SerializeField, Header("Кнопка \"Начать игру\"")]
    private Image startGameBtnImg;

    [SerializeField, Header("Image возврата в меню из победы")]
    private Image imgBackToMenuFromVictory;

    [SerializeField, Header("AudioSource для gameover")]
    private AudioSource gameOverAudioSource;

    [SerializeField, Header("AudioSource для victory")]
    private AudioSource victoryAudioSource;
    private GameObject themeItem;

    [SerializeField, Header("Пустышка")]
    private Transform[] themeItemsEmpty;
    private Material[] matSnake = new Material[5];
    private Sprite spriteOfScreenOver;
    private Sprite logoStartGame;
    private Sprite spriteOfScreenWin;

    [HideInInspector]
    private Color color;
    private Sprite counterImgSprite;
    private string mapNameTextStr;
    private string[] shopTextStrs;

    private AudioClip[] themeSounds;

    [SerializeField, Header("Рендеры змейки")]
    private Renderer[] renderersSnake;

    [SerializeField, Header("Рендеры объектов")]
    private Renderer[] renderersObjects;

    [Space(10), Header("Картинки победы/поражения"), SerializeField]
    private Image victoryImg;

    [SerializeField]
    private Image gameOverImg;

    [SerializeField, Space(10), Header("Картинка предмета")]
    private Image counterImg;

    [SerializeField, Header("Текст необходимого количества очков")]
    private TMP_Text requiresText;

    [Space(10), Header("Кнопки рестарт/победы"), SerializeField]
    private Image _btnRestartGame;

    [SerializeField, Space(10), Header("Предметы")]
    private GameObject[] Items;

    [SerializeField, Header("Лого")]
    private Image imgLogo;

    [SerializeField, Space(10), Header("Анимация змейки"), FormerlySerializedAs("animLogo")]
    private Animator animLogoSnake;

    [SerializeField, Space(10), Header("Анимация поражения")]
    private Animator animLogoGameOver;

    [SerializeField, Space(10), Header("Кнопки смены карты, сложности, предметы")]
    private Image btnChangeMapImg;

    [SerializeField]
    private GameObject btnItem;

    [SerializeField]
    private Image btnDifficult;

    [SerializeField, Space(10), Header("Название карты")]
    private TMP_Text mapNameText;

    [SerializeField, Space(10), Header("Изображение панели")]
    private Image panelImg;

    [SerializeField, Space(10), Header("Начальное изображение при старте игры")]
    private Image startGameImg;
    private bool isCameraChanged;
    private static GameObject[] themeItems = new GameObject[ThemeChanger.ThemeCount];
    private static int indexOfItem;

    [HideInInspector]
    public bool IsItemsEnabled = true;
    private bool isPaidSkin;

    [SerializeField, Header("AudioSource")]
    private AudioSource audioSource;

    [SerializeField, Header("Предупреждение об освещении")]
    private TMP_Text textHint;
    private AudioClip ostAudioClip;
    private static bool isThemePassed;
    private bool isCourTransformPassed = true;

    [HideInInspector]
    public bool IsThemeBought;

    [SerializeField]
    private ThemeChanger themeChanger;

    void Awake()
    {
        isThemePassed = false;
        themeItems = themeChanger.ThemeItems;
    }

    void Start()
    {
        foreach (var item in themeItems)
            item.SetActive(false);
        if (!Pause.IsSceneFirst)
            return;
        blockBtn = blockBtnObj.transform.parent.GetComponent<Button>();
        unlockBtn = unlockText.GetComponentInParent<Button>(true);
    }

    public void Initiliaze(
        Color color,
        Sprite counterSprite,
        AudioClip[] themeSounds,
        Sprite spriteOfScreenOver,
        Sprite spriteOfScreenWin,
        Sprite logoStartGame,
        Material[] matSnake,
        GameObject themeItem,
        AudioClip ostAudioClip,
        float price,
        int index
    )
    {
        this.color = color;
        this.counterImgSprite = counterSprite;
        this.themeSounds = themeSounds;
        this.spriteOfScreenOver = spriteOfScreenOver;
        this.spriteOfScreenWin = spriteOfScreenWin;
        this.logoStartGame = logoStartGame;
        this.matSnake = matSnake;
        this.themeItem = themeItem;
        this.ostAudioClip = ostAudioClip;
        this.price = price;
        this.index = index;
    }

    public void Initiliaze(bool isPaidSkin, bool isThemeBought)
    {
        this.isPaidSkin = isPaidSkin;
        this.IsThemeBought = isThemeBought;
    }

    public void Initiliaze(string mapNameText, string[] shopTextStrs)
    {
        this.mapNameTextStr = mapNameText;
        this.shopTextStrs = shopTextStrs;
    }

    private void SetThemeMusic(int index)
    {
        if (audioSource == null)
            return;
        if (index < 3 && !isThemePassed)
        {
            audioSource.clip = ostAudioClip;
            audioSource.Play();
            isThemePassed = true;
        }
        else if (index > 2)
        {
            audioSource.clip = ostAudioClip;
            audioSource.Play();
            isThemePassed = false;
        }
    }

    public void LockButton()
    {
        if (priceText != null)
        {
            if (IsThemeBought)
                priceText.text = shopTextStrs[0] + " " + shopTextStrs[3];
            else
                priceText.text =
                    shopTextStrs[0]
                    + " "
                    + price /*  + "\n или \n 99р" */
                ;
            unlockBtn.interactable = !IsThemeBought;
            unlockText.text = IsThemeBought ? shopTextStrs[1] : shopTextStrs[2];
        }
    }

    public void ChangeTheme(bool isBackToMenu = false)
    {
        if (animatorSpriteCounter)
            animatorSpriteCounter.SetInteger("Index", index);
        if (Multiplier)
            Multiplier.color = color;
        if (animLogoSnake != null)
            AnimateSnake(index);
        if (btnPlaymode != null)
            btnPlaymode.color = color;
        if (unlockImg != null)
            unlockImg.color = color;
        SetThemeMusic(index);
        if (blockBtnObj != null)
        {
            blockBtnObj.SetActive(!IsThemeBought);
            blockBtn.interactable = IsThemeBought;
        }
        for (int i = 0; i < matSnake.Length - 5; i++)
        {
            renderersSnake[i].sharedMaterial = matSnake[i];
            renderersObjects[i].sharedMaterial = matSnake[i + 5];
        }
        if (panelForAD != null)
        {
            panelForAD.color = new Color(color.r, color.g, color.b, 0.25f);
            panelForADInTasks.color = panelForAD.color;
        }
        if (!Pause.IsSceneFirst && requiresText != null)
        {
            requiresText.color = new Color(color.r, color.g, color.b, 0.85f);
            _btnRestartGame.color = color;
            counterImg.sprite = counterImgSprite;
            victoryImgCounter.sprite = counterImg.sprite;
            imgTask.sprite = counterImg.sprite;
            if (index == 4)
            {
                counterImg.color = new Color(0.91f, 0.56f, 0.56f);
                victoryImgCounter.color = counterImg.color;
                imgTask.color = counterImg.color;
            }
        }
        if (imgBackToMenuFromVictory != null)
            imgBackToMenuFromVictory.color = color;
        if (continueImg != null)
            continueImg.color = color;
        if (startGameBtnImg != null)
            startGameBtnImg.color = color;
        if (menuBtn != null)
            menuBtn.color = color;
        ThemeChanger.CurrentThemeColor = color;
        imgLogo.sprite = logoStartGame;
        btnChangeMapImg.color = color;
        if (btnDifficult != null)
            btnDifficult.color = color;
        if (themeSounds != null)
            ThemeChanger.ThemeSound = themeSounds[0];
        if (gameOverAudioSource != null)
        {
            gameOverAudioSource.clip = themeSounds[1];
            victoryAudioSource.clip = themeSounds[2];
        }

        if (gameOverImg != null)
        {
            gameOverImg.sprite = spriteOfScreenOver;
            victoryImg.sprite = spriteOfScreenWin;
            startGameImg.sprite = logoStartGame;
        }
        if (mapNameText != null)
        {
            SetMapName();
            mapNameText.color = new Color(color.r, color.g, color.b, 0.85f);
            if (themeNameTextInShop != null)
            {
                themeNameTextInShop.color = mapNameText.color;
                if (shopTextStrs != null)
                    LockButton();
            }
        }
        if (shopBtn != null)
            shopBtn.color = color;
        panelImg.color = color;
        for (int i = 0; i < ThemeChanger.ThemeCount; i++)
        {
            Items[i].SetActive(false);
            if (i == index)
                Items[i].SetActive(true);
        }
        if (btnItem == null)
            return;
        btnItem.GetComponent<Image>().color = color;
        btnItem.SetActive(isPaidSkin);
        ChangeThemeItem(index);
        textHint.color = color;
        textHint.enabled = ThemeChanger.ThemeNumber == 5;
    }

    public void SetMapName()
    {
        if (themeNameTextInShop != null)
            themeNameTextInShop.text = mapNameTextStr;
        mapNameText.text = mapNameTextStr;
    }

    public void AnimateSnake(int index)
    {
        if (animLogoSnake.gameObject.activeInHierarchy)
        {
            animLogoSnake.SetTrigger("Exit");
            animLogoSnake.SetInteger("IndexOfSnake", index);
        }
    }

    private void ChangeThemeItem(int index)
    {
        if (indexOfItem != index)
            themeItems[indexOfItem].SetActive(false);
        indexOfItem = Array.IndexOf(themeItems, themeItem);
    }

    public void EnableThemeItems()
    {
        IsItemsEnabled = !IsItemsEnabled;
        themeItems[indexOfItem].SetActive(IsItemsEnabled);
    }

    private void Update()
    {
        if (ThemeChanger.ThemeNumber != 3)
            return;
        if (isCourTransformPassed)
            StartCoroutine(CourTransform());
    }

    IEnumerator CourTransform()
    {
        isCourTransformPassed = false;
        if (CameraChanger.CounterCameras == 0 && isCameraChanged)
        {
            isCameraChanged = false;
            themeItems[3].transform.position = themeItemsEmpty[2].position;
        }
        else if (CameraChanger.CounterCameras == 1 && !isCameraChanged)
        {
            isCameraChanged = true;
            themeItems[3].transform.position = themeItemsEmpty[0].position;
        }
        else if (CameraChanger.CounterCameras > 1 && isCameraChanged)
            themeItems[3].transform.position = themeItemsEmpty[1].position;
        yield return new WaitForSeconds(0.01f);
        isCourTransformPassed = true;
    }
}
