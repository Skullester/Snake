using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Col : MonoBehaviour
{
    public static bool isGameOver;

    [SerializeField]
    private Volume _vol;
    private DepthOfField _dof;

    [SerializeField]
    private AudioClip _gameOverClip;

    [SerializeField]
    private GameObject _gameOver;

    [SerializeField]
    private AudioSource _audioSourceSnake;

    [SerializeField]
    private AudioSource _audioSourceGameOver;

    void Start()
    {
        isGameOver = false;
        if (_vol != null && _vol.profile.TryGet<DepthOfField>(out var tmp))
            _dof = tmp;
    }

    void OnCollisionEnter(Collision other)
    {
        GameObject obj = other.gameObject;
        if ((obj.CompareTag("Walls") || obj.CompareTag("Bodies")) && !isGameOver)
            SetGameOver();
    }

    public void SetGameOver()
    {
        AudioListener.pause = true;
        isGameOver = true;
        Cursor.lockState = CursorLockMode.None;
        _gameOver.SetActive(true);
        _dof.active = true;
        Time.timeScale = 0f;
        if (!Pause.IsOn)
            return;
        _audioSourceGameOver.ignoreListenerPause = true;
        _audioSourceGameOver.Play();
    }
}
