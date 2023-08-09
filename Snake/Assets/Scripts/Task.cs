using UnityEngine;
using TMPro;

public class Task : MonoBehaviour
{
    private Counter counterInstance;
    private Game gameInstance;
    private TMP_Text textReward;

    private TMP_Text textAbout;

    [HideInInspector]
    public float Time;

    [HideInInspector]
    private float countItems;
    private string[] textAboutRu = { "Собрать", "менее, чем за", "секунд" };
    private string[] textAboutEn = { "Collect", "in less than", "seconds" };
    private string[] textAboutTr = { "Toplayın", "daha az", "saniyede" };

    void Awake()
    {
        if (ThemeChanger.Mode == 0)
        {
            gameObject.SetActive(false);
            return;
        }
        gameInstance = GameObject.Find("Snake").GetComponent<Game>();
        textAboutRu = YG.Example.Language.CurrentLanguage switch
        {
            "en" => textAboutEn,
            "tr" => textAboutTr,
            _ => textAboutRu
        };
        counterInstance = GameObject.Find("Nose").GetComponent<Counter>();
        textAbout = transform.Find("AboutText").GetComponent<TMP_Text>();
        textReward = GetComponentInChildren<TMP_Text>();
    }

    void Update()
    {
        if (Counter.CounterInt == countItems && !Pause.IsVictory)
            counterInstance.SetVictory();
    }

    public void Initialize(float time, float items)
    {
        this.Time = time;
        this.countItems = items;
    }

    public void ShowAchievement()
    {
        gameObject.SetActive(true);
        textAbout.text = $"{textAboutRu[0]} {countItems}\n{textAboutRu[1]} {Time} {textAboutRu[2]}";
        textReward.text = "x" + " " + Game.MultiplierForTasks.ToString();
    }
}
