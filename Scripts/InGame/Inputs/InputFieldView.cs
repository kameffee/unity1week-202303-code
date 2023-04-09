using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Unity1week202303.InGame.Inputs
{
    public class InputFieldView : MonoBehaviour
    {
        [SerializeField]
        private InputField _inputField;

        private bool _isEnabled;

        public void SetEnable(bool isEnable)
        {
            _isEnabled = isEnable;
            if (isEnable)
            {
                _inputField.Select();
            }
        }

        private void Update()
        {
            if (_isEnabled)
            {
                EventSystem.current.SetSelectedGameObject(_inputField.gameObject, null);
                _inputField.OnPointerClick(new PointerEventData(EventSystem.current));
            }
        }
    }
}
