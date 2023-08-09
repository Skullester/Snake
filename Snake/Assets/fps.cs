using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fps : MonoBehaviour
{
    private TMPro.TMP_Text text;

    void Start()
    {
        text = GetComponent<TMPro.TMP_Text>();
        StartCoroutine(Cour());
    }

    IEnumerator Cour()
    {
        while (true)
        {
            text.text = ((int)(1 / Time.deltaTime)).ToString() + " FPS";
            yield return new WaitForSeconds(1);
        }
    }

    void Update() { }
}
