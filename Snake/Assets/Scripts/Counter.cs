using UnityEngine;
using TMPro;

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
        CounterInt = (int)(CounterInt * multiplierForReward);
        Pause.CounterText.text = CounterInt.ToString();
        _counterTextInTasks.text = Pause.CounterText.text;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            if (CounterInt == Pause.RequireApples)
                return;
            Destroy(other.transform.parent.gameObject);
            CounterInt++;
            SetCounters();
        }
        if (CounterInt >= Pause.RequireApples && !Pause.IsVictory)
            SetVictory();
        if (!Pause.IsVictory)
            game.CountNewApple();
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
        if (!Pause.isOn)
            return;
        _audioSource.ignoreListenerPause = true;
        _audioSource.Play();
    }
}
