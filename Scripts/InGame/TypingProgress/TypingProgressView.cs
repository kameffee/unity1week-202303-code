using TMPro;
using UnityEngine;

namespace Unity1week202303.TypingProgress
{
    /// <summary>
    /// 文章入力の進捗
    /// </summary>
    public class TypingProgressView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        [SerializeField]
        private Color _enterdColor;

        public void Set(string enteredText, string notEnteredText)
        {
            _text.SetText($"<color=#{ColorUtility.ToHtmlStringRGB(_enterdColor)}>{enteredText}</color>{notEnteredText}");
        }
    }
}
