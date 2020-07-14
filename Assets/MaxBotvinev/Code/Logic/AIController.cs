using UnityEngine;

namespace Game.Logic
{
    class AIController : MonoBehaviour
    {
        #region REFERENCES

        [SerializeField] GameController game;

        #endregion

        #region DATA

        [SerializeField] float startDelay;

        #endregion

        #region VARIABLES

        float startTime;

        #endregion

        #region UNITY EVENTS

        void Start()
        {
            startTime = Time.time;
        }

        void Update()
        {
            if(!Pause.IsPaused)
            {
                float startPeriod = Time.time - startTime;

                if (startPeriod > startDelay)
                {
                    foreach (var planet in game.Planets)
                    {
                        if (planet.IsAI)
                        {
                            planet.Turret.AimPoint = game.UserPlanet.Aim.transform.position;
                            planet.Turret.TryFire();
                        }
                    }
                }
            }
        }

        #endregion
    }
}