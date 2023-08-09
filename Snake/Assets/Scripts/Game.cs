using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class Game : MonoBehaviour
{
    [Header("AudioSource таймера")]
    public AudioSource AudioSourceTaskTimer;

    [Header("Таймер"), FormerlySerializedAs("timerTextForAchievements")]
    public TMP_Text TimerTextForTasks;

    [SerializeField, Header("Экземпляр \"Task\""), FormerlySerializedAs("achievements")]
    private Task task;

    [SerializeField]
    private GameObject textHintObj;

    [SerializeField, Range(1, 2)]
    private float DistanceBetweenHead = 1.0867f;

    [SerializeField]
    private GameObject[] _bodyPrefabsThemes;
    private GameObject _bodyPrefab;
    public List<Transform> Bodies;

    [SerializeField]
    private Transform _head;

    [SerializeField]
    private GameObject _bodiesObj;

    [SerializeField]
    private TMP_Text _timer;
    private bool isGameStarted;
    private bool isSoundStarted;
    private int tempCounter = 0;

    [SerializeField]
    private float SpeedOfMoving = 5f;

    [SerializeField]
    [Range(0, 500)]
    private float SpeedOfRotate = 3f;

    [SerializeField]
    private Image _panelImg;
    public AudioSource AudioSourceSnake;

    [SerializeField]
    private AudioClip audioClip;

    [SerializeField]
    private Animator StartGameAnim;
    private bool isClassic;
    public static float Time;
    private Col colInstance;
    public static float MultiplierForTasks;

    void Start()
    {
        colInstance = GetComponentInChildren<Col>();
        TimerTextForTasks.text = string.Empty;
        DistanceBetweenHead *= DistanceBetweenHead;
        isClassic = (int)ThemeChanger.Mode == 0;
        isGameStarted = false;
        if (!isClassic)
        {
            task.Initialize(
                GetRandomNumbers(out var countNumbers, MultiplierForTasks),
                countNumbers
            );
            RollRandomTask();
        }

        _bodyPrefab = _bodyPrefabsThemes[ThemeChanger.ThemeNumber];
        AudioSourceSnake = GetComponent<AudioSource>();
        StartCoroutine(TimerCour());
        Color color = ThemeChanger.CurrentThemeColor;
        _panelImg.color = new Color(color.r, color.g, color.b, 0.0588f);
    }

    private int GetRandomNumbers(out int countNumbers, float multiplier)
    {
        int time;
        if (MultiplierForTasks >= 1.8f)
        {
            time = Random.Range(120, 150 + 1);
            countNumbers = (int)(time * 0.52f);
        }
        else if (MultiplierForTasks >= 1.5f)
        {
            time = Random.Range(80, 110 + 1);
            countNumbers = (int)(time * 0.5f);
        }
        else if (multiplier >= 1.3f)
        {
            time = Random.Range(50, 70 + 1);
            countNumbers = (int)(time * 0.45f);
        }
        else
        {
            time = Random.Range(30, 40 + 1);
            countNumbers = (int)(time * 0.38f);
        }
        bool condition = time % 5 != 0;
        return condition ? GetRandomNumbers(out countNumbers, multiplier) : time;
    }

    public void RollRandomTask()
    {
        Time = task.Time;
        TimerTextForTasks.text = Time.ToString();
        task.ShowAchievement();
    }

    IEnumerator StartTimer()
    {
        while (Time > 0)
        {
            if (Col.isGameOver || Pause.IsVictory)
                yield break;
            Time -= UnityEngine.Time.deltaTime;
            float tmp = Mathf.Round(Time);
            TimerTextForTasks.text = tmp.ToString();
            yield return null;
        }
        Time = 0;
        colInstance.SetGameOver();
    }

    void Update()
    {
        #region Start
        if (isGameStarted)
        {
            StartGameAnim.SetBool("StartAnim", true);
            isGameStarted = false;
            StartCoroutine(StartGameCour());
        }
        if (!isSoundStarted)
            return;
        if (UnityEngine.Time.timeScale == 0 || Pause.IsVictory || Col.isGameOver)
            return;
        if (!isClassic && !AudioSourceTaskTimer.isPlaying)
            AudioSourceTaskTimer.Play();
        if (!AudioSourceSnake.isPlaying)
        {
            AudioSourceSnake.clip = ThemeChanger.ThemeSound;
            AudioSourceSnake.Play();
        }
        #endregion
        #region Game
        if (tempCounter != Counter.CounterInt && !Pause.IsVictory)
        {
            AudioSourceSnake.PlayOneShot(audioClip, 0.5f);
            tempCounter = Counter.CounterInt;
            var obj = Instantiate(
                _bodyPrefab,
                Bodies[Bodies.Count - 1].position,
                Quaternion.identity,
                _bodiesObj.transform
            );
            Bodies.Add(obj.transform);
        }
        _head.transform.position =
            _head.transform.position
            - SpeedOfMoving * UnityEngine.Time.deltaTime * _head.transform.forward;
        float angleRotation =
            Input.GetAxis("Horizontal") * SpeedOfRotate * UnityEngine.Time.deltaTime;
        _head.transform.Rotate(0f, angleRotation, 0f);
        SpeedOfMoving += 0.001f;
        if ((Bodies[0].position - _head.position).sqrMagnitude > DistanceBetweenHead)
        {
            Bodies[0].LookAt(_head.position);
            Bodies[0].position = Vector3.MoveTowards(
                Bodies[0].position,
                _head.position,
                SpeedOfMoving * UnityEngine.Time.deltaTime
            );
            for (int i = 1; i < Bodies.Count; i++)
            {
                if (
                    (Bodies[i].position - Bodies[i - 1].position).sqrMagnitude > DistanceBetweenHead
                )
                {
                    Bodies[i].LookAt(Bodies[i - 1].position);
                    Bodies[i].position = Vector3.MoveTowards(
                        Bodies[i].position,
                        Bodies[i - 1].position,
                        SpeedOfMoving * UnityEngine.Time.deltaTime
                    );
                }
            }
        }
        #endregion
    }

    public void SetAnimToDefault()
    {
        StartGameAnim.SetBool("StartAnim", false);
        StartGameAnim.SetTrigger("Exit");
    }

    public IEnumerator TimerCour()
    {
        SetAnimToDefault();
        _timer.text = "3";
        yield return new WaitForSeconds(1f);
        _timer.text = "2";
        yield return new WaitForSeconds(1f);
        _timer.text = "1";
        yield return new WaitForSeconds(1f);
        textHintObj.SetActive(false);
        _timer.text = string.Empty;
        isGameStarted = true;
        Col.isGameOver = false;
        if (!isClassic)
            StartCoroutine(StartTimer());
    }

    IEnumerator StartGameCour()
    {
        isSoundStarted = true;
        yield return new WaitForSeconds(0.6f);
    }
}
