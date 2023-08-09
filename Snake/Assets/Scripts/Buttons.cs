using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField]
    private Pause objPause;

    [SerializeField]
    private GameObject shopObj;
    private Theme themeInstance;
    private ThemeChanger themeChangerInstance;

    void Awake()
    {
        themeInstance = GetComponentInParent<Theme>();
        themeChangerInstance = GetComponentInParent<ThemeChanger>();
    }

    public void PlayGame()
    {
        objPause.Save();
        StartCoroutine(Delay());
    }

    /*
        public void ExitGame()
        {
            Application.Quit();
        } */

    public void OpenShop()
    {
        Theme.IndexOfSceneBeforeShop = ThemeChanger.ThemeNumber;
        shopObj.SetActive(true);
        themeInstance.AnimateSnake(ThemeChanger.ThemeNumber);
    }

    public void BackToMenu()
    {
        shopObj.SetActive(false);
        themeChangerInstance.ChangeTheme(Theme.IndexOfSceneBeforeShop, true);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(1);
    }
}
