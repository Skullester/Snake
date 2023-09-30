using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField]
    private Pause objPause;

    [SerializeField]
    private Animator shopAnimator;
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
        StartCoroutine(Delay(1, 0.1f));
    }

    /*
        public void ExitGame()
        {
            Application.Quit();
        } */

    public void OpenShop()
    {
        Theme.IndexOfSceneBeforeShop = ThemeChanger.ThemeNumber;
        StartAnim("IsOpen", true, true);
        themeInstance.AnimateSnake(ThemeChanger.ThemeNumber);
    }

    public void SwitchTo3D(int index)
    {
        //  LoadScreen.IsGameStarted = false;
        StartAnim("3D");
        StartCoroutine(Delay(index, 1f));
    }

    private void StartAnim(string name, bool boolState = false, bool isBool = false)
    {
        if (isBool)
            shopAnimator.SetBool(name, boolState);
        else
            shopAnimator.SetTrigger(name);
    }

    public void BackToMenu()
    {
        StartAnim("IsOpen", false, true);
        themeChangerInstance.ChangeTheme(Theme.IndexOfSceneBeforeShop, true);
        themeChangerInstance.OpenBuyingCoins(false);
    }

    IEnumerator Delay(int indexScene, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(indexScene);
    }
}
