using TMPro;
using UnityEngine;

namespace Unity1week202303.InGame.Combos
{
    public class ComboCountView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _comboCountText;

        public void Set(int count)
        {
            _comboCountText.SetText($"{count}回");
        }
    }
}
