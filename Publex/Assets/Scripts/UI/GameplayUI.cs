using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Publex.Gameplay
{
    public class GameplayUI : MonoBehaviour
    {
        [Header("Pause panel")]
        [SerializeField]
        private GameObject _pausePanelRoot;
        [SerializeField]
        private Button _pauseResume;
        [SerializeField]
        private Button _pauseSave;
        [SerializeField]
        private Button _pauseLoad;
        [SerializeField]
        private Button _pauseQuit;

        [Header("End game panel")]
        [SerializeField]
        private GameObject _endGamePanelRoot;
        [SerializeField]
        private TextMeshProUGUI _endGameStatusText;
        [SerializeField]
        private Button _endGameRestart;
        [SerializeField]
        private Button _endGameLoad;
        [SerializeField]
        private Button _endGameQuit;

        [Header("Notification")]
        [SerializeField]
        private GameObject _notificationRoot;
        [SerializeField]
        private TextMeshProUGUI _notificationText;


        [Header("Status")]
        [SerializeField]
        private GameObject _timerRoot;
        [SerializeField]
        private GameObject _attemptRoot;
        [SerializeField]
        private TextMeshProUGUI _attemptDisplay;
        [SerializeField]
        private TextMeshProUGUI _timerDisplay;

        public Button PauseResume => _pauseResume;
        public Button PauseSave => _pauseSave;
        public Button PauseLoad => _pauseLoad;
        public Button PauseQuit => _pauseQuit;
        public Button EndGameRestart => _endGameRestart;
        public Button EndGameLoad => _endGameLoad;
        public Button EndGameQuit => _endGameQuit;

        public void SetActiveStatusUI(bool isActive)
        {
            _timerRoot.SetActive(isActive);
            _attemptRoot.SetActive(isActive);
        }

        public void SetActivePausePanel(bool isActive)
        {
            _pausePanelRoot.SetActive(isActive);
        }

        public void SetActiveEndGamePanel(bool isActive, bool isWin)
        {
            _endGameLoad.gameObject.SetActive(isActive && !isWin);
            _endGamePanelRoot.SetActive(isActive);
            _endGameStatusText.text = isWin ? "You won!" : "You lose!";
        }

        public void SetAttemptText(int attempt)
        {
            _attemptDisplay.text = attempt.ToString();
        }

        public void SetTimerText(float time)
        {
            _timerDisplay.text = TimeSpan.FromSeconds(time).ToString(@"mm\:ss");
        }
        
        public void ShowNotification(string message)
        {
            _notificationRoot.gameObject.SetActive(true);
            _notificationText.text = message;
        }
    }
}
