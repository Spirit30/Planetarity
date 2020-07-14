using Game.Data.Persistance;
using Game.Logic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

namespace Game.View
{
    class MenuUIView : MonoBehaviour
    {
        #region REFERENCES

        [SerializeField] Button newButton;
        [SerializeField] Button saveButton;
        [SerializeField] Button loadButton;
        [SerializeField] Slider aiStrengthSlider;

        [SerializeField] Button pauseButton;
        [SerializeField] Button playButton;

        [SerializeField] RectTransform menuPanel;
        [SerializeField] PostProcessVolume postProcessVolume;
        [SerializeField] GameController logic;

        #endregion

        #region VARIABLES

        static bool shouldStartNewAfterReload;
        static bool shouldLoadAfterReload;

        #endregion

        #region UNITY EVENTS

        void Awake()
        {
            SubscribeToUiEvents();

            UpdateView();
        }

        void Start()
        {
            if (shouldStartNewAfterReload)
            {
                shouldStartNewAfterReload = false;

                StartNewGame();
            }
            else if (shouldLoadAfterReload)
            {
                shouldLoadAfterReload = false;

                LoadGame();
            }
        }

        #endregion

        #region UI EVENTS

        void SubscribeToUiEvents()
        {
            newButton.onClick.AddListener(OnNewButtonClick);
            saveButton.onClick.AddListener(OnSaveButtonClick);
            loadButton.onClick.AddListener(OnLoadButtonClick);
            pauseButton.onClick.AddListener(OnPauseButtonClick);
            playButton.onClick.AddListener(OnPlayButtonClick);
        }

        void OnNewButtonClick()
        {
            if(logic.IsGameStarted)
            {
                StartNewGameAfterReload();
            }
            else
            {
                StartNewGame();
            }
        }

        void OnSaveButtonClick()
        {
            if(logic.IsGameStarted)
            {
                GameState gameState = logic.ExportGameState();
                GameStatePersistance.SaveGameState(gameState);
            }
        }

        void OnLoadButtonClick()
        {
            if(logic.IsGameStarted)
            {
                LoadGameAfterReload();
            }
            else
            {
                LoadGame();
            }
        }

        void OnPauseButtonClick()
        {
            logic.TogglePause();
            UpdateView();
        }

        void OnPlayButtonClick()
        {
            logic.TogglePause();
            UpdateView();
        }

        #endregion

        #region IMPLEMENTATION

        void StartNewGame()
        {
            logic.StartNewGame();
            UpdateView();
        }

        void StartNewGameAfterReload()
        {
            shouldStartNewAfterReload = true;

            logic.Reload();
        }

        void LoadGame()
        {
            GameState gameState = GameStatePersistance.LoadGameState();
            logic.ImportGameState(gameState);
            UpdateView();
        }

        void LoadGameAfterReload()
        {
            shouldLoadAfterReload = true;

            logic.Reload();
        }

        #endregion

        #region VIEW

        void UpdateView()
        {
            ShowMenu(Pause.IsPaused);
            ShowSaveButton(logic.IsGameStarted);
            ShowLoadButton(GameStatePersistance.HasSaving());
            ShowPauseButton(!Pause.IsPaused);
            ShowPlayButton(Pause.IsPaused && logic.IsGameStarted);
            ShowDepthOfField(Pause.IsPaused);
        }

        void ShowMenu(bool flag)
        {
            menuPanel.gameObject.SetActive(flag);
        }

        void ShowSaveButton(bool flag)
        {
            saveButton.gameObject.SetActive(flag);
        }

        void ShowLoadButton(bool flag)
        {
            loadButton.gameObject.SetActive(flag);
        }

        public void ShowPauseButton(bool flag)
        {
            pauseButton.gameObject.SetActive(flag);
        }

        void ShowPlayButton(bool flag)
        {
            playButton.gameObject.SetActive(flag);
        }

        public void ShowDepthOfField(bool flag)
        {
            postProcessVolume.profile.GetSetting<DepthOfField>().active = flag;
        }

        #endregion
    }
}