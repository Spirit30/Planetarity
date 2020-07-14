using UnityEngine;

namespace Game.View
{
    class MissileView : MonoBehaviour
    {
        #region REFERENCES

        [SerializeField] MeshRenderer meshRenderer;
        [SerializeField] TrailRenderer trailRenderer;

        #endregion

        #region DATA

        [SerializeField] AudioClip sound;
        [SerializeField] float volume;

        #endregion

        #region SETTERS

        public Color Color
        {
            set
            {
                meshRenderer.material.SetColor("_EmissionColor", value);
                trailRenderer.startColor = value;
                trailRenderer.endColor = new Color(value.r, value.g, value.b, 0);
            }
        }

        #endregion

        #region INTERFACE

        public void PlaySound()
        {
            GameAudio.PlaySound(sound, volume);
        }

        public void OnPutToPool()
        {
            trailRenderer.Clear();
        }

        #endregion
    }
}