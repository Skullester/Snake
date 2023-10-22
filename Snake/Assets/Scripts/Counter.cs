using UnityEngine;
using TMPro;
using YG;

public class Counter : MonoBehaviour
{
    [SerializeField, Header("Текст счетчика в Tasks")]
    private TMP_Text _counterTextInTasks;

    [SerializeField]
    private AudioSource _audioSource;
    public static int CounterInt;

    [SerializeField]
    private GameObject _victory;

    [SerializeField]
    private Game game;

    void Start()
    {
        if (Pause.CounterText != null)
            Pause.CounterText.text = CounterInt.ToString();
    }

    public void SetCounters(float multiplierForReward = 1)
    {
        var tmp = CounterInt;
        CounterInt = (int)(CounterInt * multiplierForReward);
        YandexGame.savesData.CountOfCollectedItems += CounterInt - tmp;
        if (YandexGame.savesData.Record < CounterInt)
            YandexGame.savesData.Record = CounterInt;
        YandexGame.NewLeaderboardScores("CountOfCoins", YandexGame.savesData.CountOfCollectedItems);
        Pause.CounterText.text = CounterInt.ToString();
        _counterTextInTasks.text = Pause.CounterText.text;
        YandexGame.SaveProgress();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            if (CounterInt == Pause.RequireApples)
                return;
            Destroy(other.transform.parent.gameObject);
            CounterInt++;
            SaveProgress();
            SetCounters();
        }
        if (CounterInt >= Pause.RequireApples && !Pause.IsVictory)
            SetVictory();
        else
            game.CountNewApple();
    }

    public static void SaveProgress()
    {
        YandexGame.savesData.CountOfCollectedItems++;
        YandexGame.SaveProgress();
    }

    public void SetVictory()
    {
        if (ThemeChanger.Mode != 0)
            SetCounters(Game.MultiplierForTasks);
        Pause.IsVictory = true;
        AudioListener.pause = true;
        Cursor.lockState = CursorLockMode.None;
        _victory.SetActive(true);
        Pause.Dof.active = true;
        if (!Pause.IsOn)
            return;
        _audioSource.ignoreListenerPause = true;
        _audioSource.Play();
    }
}
