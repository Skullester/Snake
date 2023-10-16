using UnityEngine;

public class SpawnApplse : MonoBehaviour
{
    private Game gameInstance;

    private static GameObject[] items;

    private GameObject _appleTheme;

    private static Transform[] _spawnpoints;
    private bool isDuplicate;

    void Awake()
    {
        gameInstance = GameObject.Find("Snake").GetComponent<Game>();
    }

    void Start()
    {
        _spawnpoints = gameInstance.SpawnPoints;
        items = gameInstance.Items;
        _appleTheme = items[ThemeChanger.ThemeNumber];
    }

    private Vector3 CheckDublicate()
    {
        int spawnPoint = Random.Range(0, _spawnpoints.Length);
        Vector3 spawnPosition = _spawnpoints[spawnPoint].position;
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childObj = transform.GetChild(i);
            isDuplicate =
                Mathf.Approximately(childObj.position.x, spawnPosition.x)
                && Mathf.Approximately(childObj.position.z, spawnPosition.z);
            if (isDuplicate)
                break;
        }
        //print("дубликат - " + isDuplicate);
        return isDuplicate ? CheckDublicate() : spawnPosition;
    }

    void Update()
    {
        if (transform.childCount < 4)
        {
            GameObject obj = Instantiate(
                _appleTheme,
                CheckDublicate(),
                Quaternion.identity,
                transform
            );
            SetObjectPosition(obj, 4, -1.65f);
            SetObjectPosition(obj, 1, 1f);
        }
    }

    private void SetObjectPosition(GameObject obj, int index, float offset)
    {
        if (_appleTheme == items[index])
            obj.transform.position = new Vector3(
                obj.transform.position.x,
                obj.transform.position.y + offset,
                obj.transform.position.z
            );
    }
}
