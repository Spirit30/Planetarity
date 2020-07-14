using UnityEngine;

namespace Game.View
{
    public class GameAudio : MonoBehaviour
    {
        #region REFERENCES

        [SerializeField] AudioSource source;
        static GameAudio instance;

        #endregion

        #region UNITY EVENTS

        public void Start()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
                source.Play();
            }
        }

        #endregion

        #region INTERFACE

        public static void PlaySound(AudioClip clip, float volume = 1.0f)
        {
            instance.source.PlayOneShot(clip, volume);
        }

        #endregion
    }
}