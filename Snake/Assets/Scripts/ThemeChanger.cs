using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;
using System;
using YG;
using YG.Example;

public class ThemeChanger : MonoBehaviour
{
    private GameObject rewardObj;
    private Button btnPlayMode;

    [SerializeField]
    private Button btnPlayMode3D;

    [SerializeField]
    private Animator animGratitude;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private float[] prices;

    [Header("Предметы")]
    public GameObject[] ThemeItems;

    [Header("Предметы 3D")]
    public GameObject[] ThemeItems3D;

    [SerializeField, Header("Солнце")]
    private Light _sun;

    [SerializeField, Header("Саундтреки")]
    private AudioClip[] osts;

    [SerializeField, Header("Звуки меню")]
    private AudioClip[] menuSounds;
    public static int ThemeCount = 6;

    [SerializeField]
    private Material[][] materials = new Material[ThemeCount][];

    [SerializeField, Header("Материалы для классики")]
    private Material[] classic;

    [SerializeField, Header("Материалы для лавы")]
    private Material[] lava;

    [SerializeField, Header("Материалы для льда")]
    private Material[] ice;

    [SerializeField, Header("Материалы для аристократа")]
    private Material[] aristocration;

    [SerializeField, Header("Материалы для романтика"), FormerlySerializedAs("underTheMoon")]
    private Material[] romantic;

    [SerializeField, Header("Материалы для золота")]
    private Material[] gold;

    [SerializeField, Header("Текст уровня сложности")]
    private TMP_Text textDifficult;

    [SerializeField, Header("Текст режима")]
    private TMP_Text textPlaymode;

    [SerializeField]
    private TMP_Text textPlayMode3D;

    [SerializeField]
    private TMP_Text textDifficult3D;

    [SerializeField, Header("Спрайты экрана поражения")]
    private Sprite[] spritesOfScreensOver;

    [SerializeField, Header("Спрайты экрана победы")]
    private Sprite[] spritesOfScreensWin;
    private AudioClip[][] _themeSounds = new AudioClip[ThemeCount][];

    [SerializeField, Header("Звуки для классики")]
    private AudioClip[] soundClassic;

    [SerializeField, Header("Звуки для лавы")]
    private AudioClip[] soundLava;

    [SerializeField, Header("Звуки для льда")]
    private AudioClip[] soundIce;

    [SerializeField, Header("Звуки для аристократа")]
    private AudioClip[] soundAristocrat;

    [SerializeField, Header("Звуки для \"Романтики\""), FormerlySerializedAs("soundUnderTheMoon")]
    private AudioClip[] soundRomantic;

    [SerializeField, Header("Звуки для \"Золото\""), FormerlySerializedAs("soundUnderTheMoon")]
    private AudioClip[] soundGold;

    [SerializeField, Header("Спрайты счетчика")]
    private Sprite[] counterSprites;

    [SerializeField, Header("Спрайты лого")]
    private Sprite[] spriteLogos;

    [SerializeField, Header("Экземпляры класса \"Themes\"")]
    private Theme[] themes;
    private Color[] colors = new Color[ThemeCount];
    private string[] levelLanguageRu =
    {
        "Легко",
        "Средне",
        "Сложно",
        "Невозможно",
        "Свободный",
        "Классика",
        "Задания"
    };
    private string[] levelLanguageEn =
    {
        "Easy",
        "Medium",
        "Hard",
        "Impossible",
        "Free",
        "Classic",
        "Tasks"
    };
    private string[] levelLanguageTr =
    {
        "Işık",
        "Orta",
        "Zor",
        "İmkansız",
        "Özgür",
        "Klasik",
        "Görevler"
    };
    private string[] shopTextStrsRu = { "Цена:", "Разблокировано", "Разблокировать", "куплено" };
    private string[] shopTextStrsEn = { "Price:", "Unlocked", "Unlock", "bought" };
    private string[] shopTextStrsTr = { "Fiyat:", "Kilidi açıldı", "Kilidini aç", "satın alınmış" };

    [FormerlySerializedAs("textOfMap")]
    private string[] textsOfMapRu =
    {
        "Классика",
        "Лава",
        "Лёд",
        "Светский вечер",
        "Романтика",
        "Золото"
    };
    private string[] textsOfMapEn =
    {
        "Classic",
        "Lava",
        "Ice",
        "Secular evening",
        "Romance",
        "Gold"
    };
    private string[] textsOfMapTr = { "Klasik", "Lav", "Buz", "Sosyal akşam", "Romantik", "Altın" };
    private bool itemHasBeenBought;
    private bool isThemeSet = true;
    public static int ThemeNumber;
    public static AudioClip ThemeSound;
    public static Color CurrentThemeColor;
    public static LevelDifficults Level;
    public static PlayModes Mode;
    private bool isMode;

    [HideInInspector]
    public bool[] isItemBought = new bool[ThemeCount];

    public enum LevelDifficults : byte
    {
        Easy,
        Normal,
        Hard,
        Impossible,
        Free
    }

    public enum PlayModes : byte
    {
        Game,
        Tasks
    }

    void Awake()
    {
        if (Theme.is3D && UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 1)
        {
            ThemeItems = ThemeItems3D;
            textPlaymode = textPlayMode3D;
            textDifficult = textDifficult3D;
        }
        _themeSounds[0] = soundClassic;
        _themeSounds[1] = soundLava;
        _themeSounds[2] = soundIce;
        _themeSounds[3] = soundAristocrat;
        _themeSounds[4] = soundRomantic;
        _themeSounds[5] = soundGold;
        materials[0] = classic;
        materials[1] = lava;
        materials[2] = ice;
        materials[3] = aristocration;
        materials[4] = romantic;
        materials[5] = gold;
        colors[0] = new Color(0.04f, 1f, 0.22f, 0.4f);
        colors[1] = new Color(0.905f, 0.064f, 0.111f, 0.392f);
        colors[2] = new Color(0f, 1f, 1f, 0.592f);
        colors[3] = new Color(0.905f, 0.05f, 0.482f, 0.7f);
        colors[4] = new Color(0.86f, 0.24f, 0.35f, 0.64f);
        colors[5] = new Color(1f, 0.39f, 0.06f, 0.572f);
        for (int i = 0; i < ThemeCount; i++)
        {
            themes[i].Initiliaze(
                colors[i],
                counterSprites[i],
                _themeSounds[i],
                spritesOfScreensOver[i],
                spritesOfScreensWin[i],
                spriteLogos[i],
                materials[i],
                ThemeItems[i],
                osts[i],
                prices[i],
                i
            );
        }
    }

    public void RewardForGame()
    {
        if (!YandexGame.savesData.IsReward)
            return;
        YandexGame.savesData.IsReward = false;
        YandexGame.savesData.IsRewardGiven = true;
        rewardObj = transform.Find("RewardForRecord").gameObject;
        rewardObj.SetActive(true);
        int index = new();
        PlaySound(1);
        bool isAllThemesBought = true;
        for (int i = 0; i < themes.Length; i++)
            if (!themes[i].IsThemeBought)
                isAllThemesBought = false;
        if (isAllThemesBought)
            return;
        while (themes[index].IsThemeBought)
            index = UnityEngine.Random.Range(3, themes.Length);
        themes[index].IsThemeBought = true;
        YandexGame.savesData.IsThemeBought[index] = themes[index].IsThemeBought;
        YandexGame.SaveProgress();
    }

    public void CloseRewardForGame()
    {
        rewardObj.SetActive(false);
    }

    public void UnlockBtn()
    {
        itemHasBeenBought = true;
        PlaySound(0);
        themes[ThemeNumber].IsThemeBought = true;
        YandexGame.savesData.IsThemeBought[ThemeNumber] = themes[ThemeNumber].IsThemeBought;
        themes[ThemeNumber].LockButton();
        YandexGame.SaveProgress();
        for (int i = 0; i < themes.Length; i++)
            if (!themes[i].IsThemeBought)
                return;
        StartCoroutine(GratitudeCour());
    }

    public void PlaySound(int index)
    {
        audioSource.PlayOneShot(menuSounds[index]);
    }

    IEnumerator GratitudeCour()
    {
        yield return new WaitForSeconds(1f);
        animGratitude.SetTrigger("Start");
        yield return new WaitForSeconds(.5f);
        PlaySound(1);
    }

    IEnumerator StartCour()
    {
        if (YandexGame.SDKEnabled)
        {
            yield return null;
            SetLanguage();
        }
        while (!YandexGame.SDKEnabled)
        {
            yield return null;
            if (YandexGame.SDKEnabled)
                SetLanguage();
        }
    }

    public void SetLanguage()
    {
        if (Language.CurrentLanguage == "en")
        {
            textsOfMapRu = textsOfMapEn;
            shopTextStrsRu = shopTextStrsEn;
            levelLanguageRu = levelLanguageEn;
        }
        else if (Language.CurrentLanguage == "tr")
        {
            shopTextStrsRu = shopTextStrsTr;
            textsOfMapRu = textsOfMapTr;
            levelLanguageRu = levelLanguageTr;
        }
        for (int i = 0; i < ThemeCount; i++)
            themes[i].Initiliaze(textsOfMapRu[i], shopTextStrsRu);
        themes[ThemeNumber].SetMapName();
        themes[ThemeNumber].LockButton();
        if (Pause.IsSceneFirst)
        {
            Pause.IsLanguageSet = true;
            ChooseLevel();
            Game.MultiplierForTasks = (float)UnityEngine.Random.Range(11, 12 + 1) / 10;
        }
    }

    public void LoadInfoAboutPayments()
    {
        for (int i = 0; i < isItemBought.Length; i++)
        {
            if (i < 3)
            {
                isItemBought[i] = true;
                themes[i].Initiliaze(isPaidSkin: false, isItemBought[i]);
            }
            else
            {
                isItemBought[i] = YandexGame.savesData.IsThemeBought[i];
                themes[i].Initiliaze(isPaidSkin: true, isItemBought[i]);
            }
        }
        isThemeSet = false;
    }

    void Start()
    {
        if (Pause.IsSceneFirst)
            btnPlayMode = GameObject.Find("PlaymodeBtn").GetComponent<Button>();
        if (Theme.is3D)
        {
            btnPlayMode = btnPlayMode3D;
        }
        StartCoroutine(StartCour());
        if (PlayerPrefs.HasKey("0"))
        {
            for (int i = 0; i < ThemeCount; i++)
                themes[i].IsItemsEnabled = Convert.ToBoolean(PlayerPrefs.GetInt(i.ToString()));
        }
    }

    private void Update()
    {
        if (!isThemeSet)
            ChangeTheme();
    }

    public void ChangeTheme(int themeNum = -1, bool isBackToMenu = false)
    {
        isThemeSet = true;
        ThemeNumber = themeNum == -1 || itemHasBeenBought ? ThemeNumber : themeNum;
        itemHasBeenBought = false;
        themes[ThemeNumber].ChangeTheme(isBackToMenu);
        ThemeItems[ThemeNumber].SetActive(GetStateItem());
        bool isThemeAndSun = ThemeNumber != 5 && Pause.IsSun;
        _sun.enabled = isThemeAndSun;
    }

    public bool GetStateItem()
    {
        return themes[ThemeNumber].IsItemsEnabled;
    }

    IEnumerator CourItem()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < ThemeCount; i++)
            PlayerPrefs.SetInt(i.ToString(), themes[i].IsItemsEnabled ? 1 : 0);
    }

    public void ChangeThemeItem()
    {
        StartCoroutine(CourItem());
        themes[ThemeNumber].EnableThemeItems();
    }

    public void ChangeMap(int chosenThemeNumber)
    {
        isThemeSet = false;
        if (chosenThemeNumber != 0)
        {
            ThemeNumber = chosenThemeNumber;
            return;
        }
        ThemeNumber++;
        if (ThemeNumber == ThemeCount)
            ThemeNumber = 0;
    }

    private int GetLengthOfEnum(Type type)
    {
        return Enum.GetNames(type).Length;
    }

    public void ChangeDifficult(bool isClassic)
    {
        int lengthDifficult = GetLengthOfEnum(typeof(LevelDifficults));
        int lengthPlayMode = GetLengthOfEnum(typeof(PlayModes));
        bool isModePreLast = (int)Mode == lengthPlayMode - 1;
        bool isClassicPreLast;
        if (isClassic)
        {
            if (isMode)
            {
                Mode++;
                isMode = false;
            }
            Level++;
            isClassicPreLast = (int)Level == lengthDifficult - 1;
            if (isClassicPreLast)
            {
                btnPlayMode.interactable = false;
                if (isModePreLast)
                {
                    isMode = true;
                    Mode = 0;
                }
            }
            else
                btnPlayMode.interactable = true;
            if (Level == (LevelDifficults)lengthDifficult)
                Level = 0;
        }
        else
        {
            Mode++;
            if (Mode == (PlayModes)lengthPlayMode)
                Mode = 0;
        }
        ChooseLevel();
    }

    private void SetLevel(byte index, int countApplesColor, Color color)
    {
        Game.MultiplierForTasks = index switch
        {
            0 => (float)UnityEngine.Random.Range(11, 12 + 1) / 10,
            1 => (float)UnityEngine.Random.Range(13, 14 + 1) / 10,
            2 => (float)UnityEngine.Random.Range(15, 17 + 1) / 10,
            3 => (float)UnityEngine.Random.Range(18, 20 + 1) / 10,
            _ => 0
        };
        textDifficult.color = color;
        textDifficult.text = levelLanguageRu[index];
        Pause.RequireApples = countApplesColor;
    }

    private void ChooseLevel()
    {
        switch (Level)
        {
            case LevelDifficults.Easy:
                SetLevel((byte)Level, 25, Color.white);
                break;
            case LevelDifficults.Normal:
                SetLevel((byte)Level, 50, Color.white);
                break;
            case LevelDifficults.Hard:
                SetLevel((byte)Level, 100, Color.white);
                break;
            case LevelDifficults.Impossible:
                SetLevel((byte)Level, 150, Color.red);
                break;
            case LevelDifficults.Free:
                SetLevel((byte)Level, (int)Mathf.Pow(10, 3), Color.white);
                break;
        }
        switch (Mode)
        {
            case PlayModes.Game:
                textPlaymode.text = levelLanguageRu[5];
                break;
            case PlayModes.Tasks:
                Pause.RequireApples = Mathf.Pow(10, 6);
                textPlaymode.text = levelLanguageRu[6];
                break;
        }
    }
}
