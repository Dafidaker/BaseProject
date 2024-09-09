using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Utility.UI
{
    public class DeselectButtonOnClick : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();

            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}