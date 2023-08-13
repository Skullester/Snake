using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System;
using YG;
using YG.Example;

public class Pause : MonoBehaviour
{
    private TMP_Dropdown dropdown;

    [SerializeField, Header("Текст анимации reward")]
    private TMP_Text rewardText;

    [SerializeField, Header("Текст собранных предметов в магазине")]
    private TMP_Text textCollectedInShop;

    [SerializeField, Header("Звук блока")]
    private AudioClip blockSound;
    private AudioClip clipTimer;

    [SerializeField, Header("Экземпляр класса \"CameraChanger\"")]
    private CameraChanger cameraChanger;

    [SerializeField, Header("Текст \"Рекорд\"")]
    private TMP_Text textRecord;

    [SerializeField, Header("Текст \"Собрано\"")]
    private TMP_Text textCollectedItems;
    private int countOfCollectedItems;

    [SerializeField, Header("Экземпляр класса \"Game\"")]
    private Game gameObj;

    [SerializeField, Header("Голова змейки")]
    private GameObject head;

    [SerializeField, Header("Кнопка рекламы")]
    private GameObject buttonAD;

    [SerializeField, Header("Кнопка рекламы в Tasks")]
    private GameObject buttonADInTasks;

    [Header("Счетчик предметов")]
    public static TMP_Text CounterText;

    [SerializeField, Header("Чекпоинт")]
    private Transform checkPoint;

    [SerializeField, Header("Тела змейки")]
    private Transform bodies;
    private bool isOn;
    public static bool IsSun = true;
    public static bool IsVictory;
    public static bool IsLanguageSet;

    [SerializeField, Header("Экран поражения")]
    private GameObject _gameOver;

    [SerializeField, Header("Аниматоры")]
    private Animator BackToMenuAnim;

    [SerializeField]
    private Animator victoryBackToMenuAnim;

    [SerializeField]
    private Animator RestartGameAnim;

    [SerializeField, Header("Меню")]
    private GameObject menu;

    [SerializeField, Header("Солнце")]
    private Light _sun;

    [SerializeField, Header("Картинка звука")]
    private Image _soundImg;

    [SerializeField]
    private AudioSource _audioSourceSnake;

    [SerializeField, Header("Спрайты звука")]
    private Sprite[] soundMute;

    [SerializeField, Header("Текст количества необходимых предметов")]
    private TMP_Text _requiresText;
    public static float RequireApples = 3;

    [SerializeField]
    private Image gameOverBackToMenuBtn;

    [SerializeField, Header("Volume")]
    private Volume _vol;
    public static DepthOfField Dof;
    public static bool IsSceneFirst;
    public static bool IsScene3D;
    private ThemeChanger themeChanger;
    private static bool isGraphicsSet;
    private string[] ruGraphicsTexts = { "Настройки графики: ", "Низкие", "Средние", "Высокие" };
    private string[] enGraphicsTexts = { "Graphics settings: ", "Low", "Medium", "High" };
    private string[] trGraphicsTexts = { "Grafik ayarları: ", "Düşük", "Orta", "Yüksek" };

    private void OnEnable() => YandexGame.GetDataEvent += LoadSettings;

    private void OnDisable() => YandexGame.GetDataEvent -= LoadSettings;

    public static TMP_Text GraphicsText;
    private Animator animatorGraphics;
    private static int graphicsIndex = 2;

    private void Awake()
    {
        IsScene3D = SceneManager.GetActiveScene().buildIndex == 2;
        IsLanguageSet = false;
        themeChanger = GetComponent<ThemeChanger>();
        IsVictory = false;
        IsSceneFirst = SceneManager.GetActiveScene().buildIndex == 0;
        if (!IsSceneFirst)
        {
            if (IsScene3D)
                return;
            clipTimer = _audioSourceSnake.clip;
            CounterText = GetComponentInChildren<TMP_Text>();
            return;
        }
        GraphicsText = transform.Find("GraphicsText").GetComponent<TMP_Text>();
        animatorGraphics = GraphicsText.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
            ScreenCapture.CaptureScreenshot("Screen.png");
        if (!IsSceneFirst && !Col.isGameOver && !IsVictory && Input.GetKeyDown(KeyCode.Tab))
            SetPauseOptions(true, 0);
    }

    public void PlayBlockSound()
    {
        _audioSourceSnake.PlayOneShot(blockSound);
    }

    private IEnumerator AwakeCour()
    {
        while (!YandexGame.SDKEnabled)
        {
            yield return null;
            if (YandexGame.SDKEnabled)
                LoadSettings();
        }
    }

    private void LoadSettings()
    {
        bool isObjectsInitiliazed = themeChanger.isItemBought[0];
        if (Language.CurrentLanguage != null)
        {
            YandexGame.SwitchLanguage("en");
            ruGraphicsTexts = YG.Example.Language.CurrentLanguage switch
            {
                "en" => enGraphicsTexts,
                "tr" => trGraphicsTexts,
                _ => ruGraphicsTexts
            };
        }
        if (!isObjectsInitiliazed)
            themeChanger.LoadInfoAboutPayments();
        countOfCollectedItems = YandexGame.savesData.CountOfCollectedItems;
        if (textCollectedItems != null)
            textCollectedItems.text += $" {countOfCollectedItems}";
        if (Pause.IsScene3D)
            return;
        themeChanger.RewardForGame();
        CameraChanger.CounterCameras = YandexGame.savesData.CounterCameras;
        cameraChanger.CameraManager();
        isOn = YandexGame.savesData.IsMuted;
        AudioListener.pause = isOn;
        _soundImg.sprite = isOn ? soundMute[0] : soundMute[1];
        IsSun = YandexGame.savesData.IsSunOn;
        _sun.enabled = IsSun;
        if (textCollectedInShop != null)
            textCollectedInShop.text = textCollectedItems.text;
        if (textRecord != null)
            textRecord.text += $" {YandexGame.savesData.Record}";
        if (IsSceneFirst || Pause.IsScene3D)
        {
            if (!isGraphicsSet)
            {
                graphicsIndex = YandexGame.savesData.indexOfQuality;
                isGraphicsSet = true;
                SetGraphics(graphicsIndex);
            }
            return;
        }
        if (ThemeChanger.Mode == 0)
            _requiresText.text += " " + RequireApples.ToString();
        else
            _requiresText.text = string.Empty;
    }

    public void Save()
    {
        IsVictory = true;
        Time.timeScale = 1f;
        if (!isOn)
            AudioListener.pause = false;
        YandexGame.savesData.IsMuted = isOn;
        YandexGame.savesData.IsSunOn = IsSun;
        YandexGame.savesData.CounterCameras = CameraChanger.CounterCameras;
        if (!IsSceneFirst)
        {
            YandexGame.savesData.CountOfCollectedItems += Counter.CounterInt;
            YandexGame.NewLeaderboardScores(
                "ItemsCollected",
                YandexGame.savesData.CountOfCollectedItems
            );
            if (Counter.CounterInt > YandexGame.savesData.Record)
                YandexGame.savesData.Record = Counter.CounterInt;
            if ( /* Counter.CounterInt > 300 &&  */
                !YandexGame.savesData.IsRewardGiven)
                YandexGame.savesData.IsReward = true;
        }
        else
            YandexGame.savesData.indexOfQuality = graphicsIndex;
        YandexGame.SaveProgress();
    }

    void Start()
    {
        StartCoroutine(AwakeCour());
        if (YandexGame.SDKEnabled)
            LoadSettings();
        if (IsSceneFirst || Pause.IsScene3D)
            return;
        gameOverBackToMenuBtn.color = ThemeChanger.CurrentThemeColor;
        Cursor.lockState = CursorLockMode.Locked;
        if (_vol.profile.TryGet<DepthOfField>(out var tmp))
            Dof = tmp;
    }

    public void SetGraphics(int index = -1)
    {
        if (index == -1)
        {
            graphicsIndex++;
            if (graphicsIndex == 3)
                graphicsIndex = 0;
            GraphicsText.text = ruGraphicsTexts[0];
            GraphicsText.text += ruGraphicsTexts[graphicsIndex + 1];
            if (!animatorGraphics.GetCurrentAnimatorStateInfo(0).IsName("GraphicsApperance"))
                animatorGraphics.SetTrigger("Graphics");
        }
        QualitySettings.SetQualityLevel(graphicsIndex);
    }

    public void Reward()
    {
        themeChanger.PlaySound(0);
        if (IsVictory)
            buttonAD = buttonADInTasks;
        buttonAD.SetActive(false);
        if (IsVictory)
        {
            rewardText.gameObject.SetActive(true);
            rewardText.text += ((int)(Counter.CounterInt * 0.3f)).ToString();
            rewardText.GetComponent<Animator>().SetTrigger("Trigger");
            return;
        }

        if (Game.Time < 10 && ThemeChanger.Mode != 0)
        {
            Game.Time = Mathf.Round(Game.Time) + 15;
            gameObj.TimerTextForTasks.text = Game.Time.ToString();
        }
        gameObj.AudioSourceTaskTimer.Stop();
        _audioSourceSnake.Stop();
        _audioSourceSnake.PlayOneShot(clipTimer);
        head.transform.position = checkPoint.position;
        bodies.position = checkPoint.position;
        _gameOver.SetActive(false);
        Dof.active = false;
        AudioListener.pause = false;
        head.transform.localRotation = Quaternion.Euler(0, -180, 0);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        for (int i = 0; i < gameObj.Bodies.Count; i++)
        {
            gameObj.Bodies[i].transform.rotation = Quaternion.identity;
            if (i > 0)
                gameObj.Bodies[i].position =
                    gameObj.Bodies[i - 1].position + new Vector3(0, 0, 1.08f);
            else
                gameObj.Bodies[i].position = head.transform.position + new Vector3(0, 0, 1.2f);
        }
        StartCoroutine(gameObj.TimerCour());
    }

    public void CloseAD()
    {
        if (IsVictory)
            AudioListener.pause = true;
    }

    private void TestButton(ref float variable, float value)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Counter.CounterInt = 340;
            variable = value;
        }
    }

    public void CloseMenu()
    {
        SetPauseOptions(false, 0);
    }

    public void SetAD(bool isOn)
    {
        if (isOn)
            _audioSourceSnake.Pause();
        else
            _audioSourceSnake.UnPause();
    }

    private void SetPauseOptions(bool condition, int index)
    {
        Time.timeScale = Convert.ToInt32(!condition);
        Cursor.lockState = condition ? CursorLockMode.None : CursorLockMode.Locked;
        Dof.active = condition;
        if (condition)
            _audioSourceSnake.Pause();
        else
            _audioSourceSnake.UnPause();
        if (index != 0)
            return;
        if (isOn)
            AudioListener.pause = true;
        else
            AudioListener.pause = condition;
        menu.SetActive(condition);
    }

    public void BackToMenu()
    {
        if (IsVictory)
        {
            AudioListener.pause = false;
            _audioSourceSnake.Play();
        }
        Save();
        BackToMenuAnim.SetTrigger("BackToMenu");
        StartCoroutine(BackToMenuCour());
    }

    public void BackToMenuVictory()
    {
        Save();
        if (RestartGameAnim.gameObject.activeSelf)
            RestartGameAnim.SetTrigger("Restart");
        else
            victoryBackToMenuAnim.SetTrigger("Victory");
        StartCoroutine(BackToMenuCour());
    }

    public void RestartGame()
    {
        Save();
        RestartGameAnim.SetTrigger("Restart");
        StartCoroutine(RestartGameCour());
    }

    public void SoundMute()
    {
        isOn = !isOn;
        if (SceneManager.GetActiveScene().buildIndex != 2 && menu.activeSelf && !IsSceneFirst)
            AudioListener.pause = true;
        else
            AudioListener.pause = isOn;
        _soundImg.sprite = isOn ? soundMute[0] : soundMute[1];
    }

    public void ControlLighting()
    {
        if (ThemeChanger.ThemeNumber == 5)
            return;
        IsSun = !IsSun;
        _sun.enabled = IsSun;
    }

    IEnumerator BackToMenuCour()
    {
        yield return new WaitForSeconds(1f);
        LeaveGameSession(0);
    }

    IEnumerator RestartGameCour()
    {
        yield return new WaitForSeconds(1f);
        LeaveGameSession(1);
    }

    private void LeaveGameSession(int indexScene)
    {
        Counter.CounterInt = 0;
        SceneManager.LoadScene(indexScene);
    }
}
