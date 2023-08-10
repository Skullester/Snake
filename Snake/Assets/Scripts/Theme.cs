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

    [SerializeField, Header("Кнопка блока")]
    private Button blockBtn;
    private int index;
    public static int IndexOfSceneBeforeShop;

    [Header("Магазин")]
    [SerializeField, Header("Текст цены")]
    private TMP_Text priceText;

    [SerializeField, Header("Текст кнопки \"Разблокировать\"")]
    private TMP_Text unlockText;

    [SerializeField, Header("Кнопка покупки")]
    private Button unlockBtn;
    private float price;

    [Space(10f)]
    [SerializeField, Header("Кнопка магазина")]
    private Image shopBtn;

    [SerializeField, Header("Панелька для кнопки рекламы")]
    private Image panelForAD;

    [SerializeField, Header("Панелька для кнопки рекламы в Tasks")]
    private Image panelForADInTasks;

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

    private Material[] mat3D = new Material[5];
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
    private static bool is3D;

    [Header("Ссылки на 3D:")]
    [SerializeField]
    private Image btnPlaymode3D;

    [SerializeField]
    private Image unlockIm3D;

    [SerializeField]
    private GameObject blockBtnObj3D;

    [SerializeField]
    private Button blockBtn3D;

    [SerializeField]
    private Image startGameBtnImg3D;

    [SerializeField]
    private Image imgLogo3D;

    [SerializeField]
    private Image btnDifficult3D;

    [SerializeField]
    private Image shopBtn3D;

    [SerializeField]
    private TMP_Text textHint3D;

    [SerializeField]
    private Image panelImg3D;

    [SerializeField]
    private Image btnChangeMapImg3D;

    [SerializeField]
    private TMP_Text mapNameText3D;

    void Awake()
    {
        /*   is3D = true; */
        ChangeReferences();
        isThemePassed = false;
        themeItems = themeChanger.ThemeItems;
    }

    void Start()
    {
        foreach (var item in themeItems)
            item.SetActive(false);
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

    public void Initiliaze(Material[] mat3D)
    {
        this.mat3D = mat3D;
    }

    public void Initiliaze(string mapNameText, string[] shopTextStrs)
    {
        this.mapNameTextStr = mapNameText;
        this.shopTextStrs = shopTextStrs;
    }

    private void SetThemeMusic(int index)
    {
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

    public void Set3DOptions()
    {
        for (int i = 0; i < mat3D.Length; i++)
            renderersObjects[i].sharedMaterial = mat3D[i];
    }

    public void LockButton()
    {
        if (!Pause.IsSceneFirst)
            return;
        if (IsThemeBought)
            priceText.text = shopTextStrs[0] + " " + shopTextStrs[3];
        else
            priceText.text = shopTextStrs[0] + " " + price;
        unlockBtn.interactable = !IsThemeBought;
        unlockText.text = IsThemeBought ? shopTextStrs[1] : shopTextStrs[2];
    }

    private void ChangeSecondScene()
    {
        animatorSpriteCounter.SetInteger("Index", index);
        Multiplier.color = color;
        panelForAD.color = new Color(color.r, color.g, color.b, 0.25f);
        panelForADInTasks.color = panelForAD.color;
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
        imgBackToMenuFromVictory.color = color;
        continueImg.color = color;
        ThemeChanger.ThemeSound = themeSounds[0];
        gameOverAudioSource.clip = themeSounds[1];
        victoryAudioSource.clip = themeSounds[2];
        gameOverImg.sprite = spriteOfScreenOver;
        victoryImg.sprite = spriteOfScreenWin;
        startGameImg.sprite = logoStartGame;
    }

    private void Set3DMenu()
    {
        is3D = !is3D;
    }

    private void ChangeReferences()
    {
        if (is3D)
        {
            btnPlaymode = btnPlaymode3D;
            panelImg = panelImg3D;
            btnChangeMapImg = btnChangeMapImg3D;
            mapNameText = mapNameText3D;
            blockBtnObj = blockBtnObj3D;
            blockBtn = blockBtn3D;
            startGameBtnImg = startGameBtnImg3D;
            imgLogo = imgLogo3D;
            btnDifficult = btnDifficult3D;
            shopBtn = shopBtn3D;
            textHint = textHint3D;
        }
    }

    private void ChangeFirstScene()
    {
        btnPlaymode.color = color;
        unlockImg.color = color;
        SetThemeMusic(index);
        blockBtnObj.SetActive(!IsThemeBought);
        blockBtn.interactable = IsThemeBought;
        startGameBtnImg.color = color;
        imgLogo.sprite = logoStartGame;
        btnDifficult.color = color;
        shopBtn.color = color;
        themeNameTextInShop.color = mapNameText.color;
        if (Pause.IsLanguageSet)
            LockButton();
        btnItem.GetComponent<Image>().color = color;
        btnItem.SetActive(isPaidSkin);
        ChangeThemeItem(index);
        textHint.color = color;
        textHint.enabled = ThemeChanger.ThemeNumber == 5;
    }

    public void ChangeTheme(bool isBackToMenu = false)
    {
        ThemeChanger.CurrentThemeColor = color;
        panelImg.color = color;
        AnimateSnake(index);
        btnChangeMapImg.color = color;
        for (int i = 0; i < matSnake.Length - 5; i++)
        {
            renderersSnake[i].sharedMaterial = matSnake[i];
            renderersObjects[i].sharedMaterial = matSnake[i + 5];
        }
        SetItems();
        SetMapName();
        mapNameText.color = new Color(color.r, color.g, color.b, 0.85f);
        if (Pause.IsSceneFirst)
            ChangeFirstScene();
        else
            ChangeSecondScene();
    }

    public void SetItems(bool condition = true)
    {
        for (int i = 0; i < ThemeChanger.ThemeCount; i++)
        {
            bool isThemeItem = i == index && condition;
            Items[i].SetActive(isThemeItem);
        }
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
        if (isCourTransformPassed && ThemeChanger.ThemeNumber == 3)
            StartCoroutine(CourTransform());
    }

    IEnumerator CourTransform()
    {
        isCourTransformPassed = false;
        while (true)
        {
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
            yield return null;
        }
    }
}
