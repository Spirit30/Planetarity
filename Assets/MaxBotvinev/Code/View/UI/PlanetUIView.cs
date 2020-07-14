using UnityEngine;
using UnityEngine.UI;

namespace Game.View
{
    class PlanetUIView : MonoBehaviour
    {
        #region REFERENCES

        [SerializeField] Text nameLable;
        [SerializeField] Image lifeBarImage;

        #endregion

        #region GETTERS

        public string Name => nameLable.text;

        #endregion

        #region SETTERS

        public void Init(string name, Color color, float size)
        {
            nameLable.text = name;
            nameLable.color =
                lifeBarImage.color = color;
            transform.localScale = Vector3.one / size;
        }

        public void SetLife(float lifeProgress)
        {
            lifeBarImage.fillAmount = Mathf.Clamp01(lifeProgress);
        }

        #endregion
    }
}