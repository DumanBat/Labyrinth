using Publex.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Publex.Gameplay
{
    public class GameMediator : MonoBehaviour
    {
        [Header("General")]
        [SerializeField]
        private GameConfig _config;
        [SerializeField]
        private DataHolder _dataHolder;

        [Space]
        [Header("References")]
        [SerializeField]
        private MobileInputView _mobileInputView;
        [SerializeField]
        private CameraFollow _cameraFollow;
        [SerializeField]
        private GameplayUI _gameplayUI;
        [SerializeField]
        private CountdownTimer _timer;

        private StartDataProvider _startDataProvider;
        private PlayerController _player;
        private LevelConfig _currentLevelConfig;
        private Level _currentLevel;
        private int _attempts;

        public Action<bool> GameFinished;

        private void Awake()
        {
            _mobileInputView.SetActiveInputView(false);
            _gameplayUI.SetActiveStatusUI(false);
            _gameplayUI.SetActiveEndGamePanel(false, false);

            _startDataProvider = new StartDataProvider(_dataHolder, _config);
            Subscribe();
        }

        private void Start()
        {
            _attempts = 1;
            _gameplayUI.SetAttemptText(_attempts);
            var startData = _startDataProvider.GetStartData(_dataHolder.loadSaved);
            StartGame(startData);
        }

        private void Subscribe()
        {
            GameFinished += FinishGame;

            _mobileInputView.Pause.onClick.AddListener(() => PauseGame(true));

            _timer.RemainingTimeChanged += _gameplayUI.SetTimerText;

            _gameplayUI.PauseResume.onClick.AddListener(() => PauseGame(false));
            _gameplayUI.PauseSave.onClick.AddListener(() => SaveGame());
            _gameplayUI.PauseLoad.onClick.AddListener(() => LoadGame());
            _gameplayUI.PauseQuit.onClick.AddListener(() => Application.Quit());

            _gameplayUI.EndGameRestart.onClick.AddListener(() => RestartGame());
            _gameplayUI.EndGameLoad.onClick.AddListener(() => LoadGame());
            _gameplayUI.PauseQuit.onClick.AddListener(() => Application.Quit());
        }

        private void StartGame(GameStartParameters startData)
        {
            _currentLevelConfig = startData.levelConfig;
            LoadLevel(_currentLevelConfig);
            LoadPlayer();
            SetupPlayer(startData.playerPosition, startData.health);

            _timer.StartTimer(startData.timeLeft, FinishGame);
            _cameraFollow.Init(_player.transform, _player.BasicCameraOffset);

            _gameplayUI.SetActiveStatusUI(true);
            _gameplayUI.SetActiveEndGamePanel(false, false);
            _mobileInputView.SetActiveInputView(true);
        }

        private void LoadLevel(LevelConfig levelConfig)
        {
            _currentLevel = Instantiate(levelConfig.LevelPrefab);
            _currentLevel.InitLevel(this);
        }

        private void LoadPlayer()
        {
            _player = Instantiate(_config.PlayerPrefab);
            _player.Init(_config, _mobileInputView, FinishGame);
        }

        private void SetupPlayer(Vector3 pos, float health)
        {
            _player.transform.position = pos;
            _player.Health.Health = health;
        }

        private void PauseGame(bool isPause)
        {
            Time.timeScale = isPause ? 0f : 1f;
            _gameplayUI.SetActivePausePanel(isPause);
            _mobileInputView.SetActiveInputView(!isPause);
        }

        private void SaveGame()
        {
            SaveManager.Instance.SaveData(GetSaveData());
        }

        private void LoadGame()
        {
            var savedData = SaveManager.Instance.LoadData();

            if (savedData == null)
            {
                _gameplayUI.ShowNotification("Save file not found!");
                return;
            }

            _currentLevel.Unload();
            _player.Unload();
            _timer.StopTimer();
            PauseGame(false);

            var startData = _startDataProvider.GetStartData(savedData);
            StartGame(startData);
        }

        private void FinishGame(bool isWin)
        {
            _player.Disable();
            _timer.StopTimer();
            _currentLevel.StopLevel();

            if (isWin)
            {
                _attempts = 0;
                _gameplayUI.SetAttemptText(_attempts);
            }

            _mobileInputView.SetActiveInputView(false);
            _gameplayUI.SetActiveStatusUI(false);
            _gameplayUI.SetActiveEndGamePanel(true, isWin);
        }

        private void RestartGame()
        {
            _currentLevel.Unload();
            _player.Unload();

            _attempts += 1;
            _gameplayUI.SetAttemptText(_attempts);

            var startData = _startDataProvider.GetStartData(_currentLevelConfig);
            StartGame(startData);
        }

        private SaveData GetSaveData()
        {
            var data = new SaveData()
            {
                levelId = _currentLevelConfig.LevelId,
                playerPosition = _player.transform.position,
                timeLeft = _timer.GetRemainingTime(),
                health = _player.Health.Health
            };

            return data;
        }
    }
}
