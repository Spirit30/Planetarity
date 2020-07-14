using Game.Logic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View
{
    class GameOverUIView : MonoBehaviour
    {
        #region REFERENCES

        [SerializeField] Text statusLable;
        [SerializeField] Button okButton;
        [SerializeField] GameController logic;

        #endregion

        #region DATA

        [SerializeField] string winText;
        [SerializeField] string loseText;

        #endregion

        #region INTERFACE

        public void Open(bool isWin, Color color)
        {
            statusLable.text = isWin ? winText : loseText;
            statusLable.color = color;
            okButton.onClick.AddListener(OnOkButton);
            gameObject.SetActive(true);
        }

        #endregion

        #region UI EVENTS

        void OnOkButton()
        {
            logic.Reload();
        }

        #endregion
    }
}