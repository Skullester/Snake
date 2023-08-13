using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fps : MonoBehaviour
{
    [SerializeField]
    private Texture2D texture;
    private TMPro.TMP_Text text;

    [SerializeField]
    private bool isFps;

    void Start()
    {
        text = GetComponent<TMPro.TMP_Text>();
        text.text = string.Empty;
        if (isFps)
            StartCoroutine(Cour());
    }

    IEnumerator Cour()
    {
        while (true)
        {
            text.text = ((int)(1 / Time.deltaTime)).ToString() + " FPS";
            yield return new WaitForSeconds(0.75F);
        }
    }
}
