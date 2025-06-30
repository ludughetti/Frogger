using System;
using UnityEngine;
using Utils;

namespace UI.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private MainMenuView view;

        private MainMenuPresenter _presenter;
        
        private void Start()
        {
            _presenter = new MainMenuPresenter(view, new SceneLoader(), new ApplicationHandler());
        }

        private void OnDestroy()
        {
            _presenter.Dispose();
        }
    }
}
