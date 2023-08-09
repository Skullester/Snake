using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    public static int CounterCameras = 0;
    private int countOfCameras = 5;

    [SerializeField]
    private GameObject[] _cams;

    private void Start()
    {
        CameraManager();
    }

    public void CameraManager()
    {
        for (int i = 0; i < countOfCameras; i++)
        {
            _cams[i].SetActive(false);
            if (i == CounterCameras)
                _cams[i].SetActive(true);
        }
    }

    public void ChangeCamera()
    {
        _cams[CounterCameras].SetActive(false);
        CounterCameras++;
        if (CounterCameras == countOfCameras)
        {
            CounterCameras = 0;
        }
        _cams[CounterCameras].SetActive(true);
    }
}
