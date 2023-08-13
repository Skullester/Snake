using UnityEngine;
using TMPro;

namespace YG.Example
{
    public class Language : MonoBehaviour
    {
        [SerializeField, TextArea]
        private string[] ru;

        [SerializeField, TextArea]
        private string[] en;

        [SerializeField, TextArea]
        private string[] tr;

        [SerializeField]
        private TMP_Text[] texts;
        public static string CurrentLanguage;

        private void OnEnable() => YandexGame.SwitchLangEvent += SwitchLanguage;

        private void OnDisable() => YandexGame.SwitchLangEvent -= SwitchLanguage;

        public void SwitchLanguage(string lang)
        {
            print(lang);
            CurrentLanguage = lang;
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].text = lang switch
                {
                    "en" => en[i],
                    "tr" => tr[i],
                    _ => ru[i]
                };
            }
        }
    }
}
