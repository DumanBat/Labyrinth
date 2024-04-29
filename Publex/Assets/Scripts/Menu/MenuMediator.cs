using Publex.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Publex.Menu
{
    public class MenuMediator : MonoBehaviour
    {
        private const string GameScene = "Game";

        [Header("General")]
        [SerializeField]
        private DataHolder _dataHolder;

        [Space]
        [Header("References")]
        [SerializeField]
        private MenuUI _menuUI;

        private void Awake()
        {
            _dataHolder.loadSaved = false;
            _dataHolder.saveData = null;

            Subscribe();
        }

        private void Subscribe()
        {
            _menuUI.Start.onClick.AddListener(() => LoadGameScene());
            _menuUI.Load.onClick.AddListener(() => Load());
            _menuUI.Quit.onClick.AddListener(() => Application.Quit());
        }

        private void Load()
        {
            var data = SaveManager.Instance.LoadData();            
            if (data == null)
            {
                _menuUI.SetStatusText("Save file not found!");
                return;
            }

            _dataHolder.saveData = data;
            _dataHolder.loadSaved = true;
            LoadGameScene();
        }

        private void LoadGameScene() => SceneManager.LoadScene(GameScene);
    }
}
