using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Publex.Menu
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField]
        private Button _start;
        [SerializeField]
        private Button _load;
        [SerializeField]
        private Button _quit;

        [SerializeField]
        private TextMeshProUGUI _statusText;

        public Button Start => _start;
        public Button Load => _load;
        public Button Quit => _quit;

        public void SetStatusText(string text)
        {
            _statusText.text = text;
        }
    }
}
