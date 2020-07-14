using UnityEngine;

namespace Game.Logic.Input
{
    class InputController : MonoBehaviour
    {
        #region REFERENCES

        [SerializeField] GameController game;

        #endregion

        #region UNITY EVENTS

        void Update()
        {
            if(!Pause.IsPaused)
            {
                game.UserPlanet.Turret.AimPoint = GetInputPoint();

                if (ShouldFire())
                {
                    game.UserPlanet.Turret.TryFire();
                }
            }
        }

        #endregion

        #region IMPLEMENTATION

        /// <summary>
        /// Good for both Mobile & Desktop
        /// </summary>
        Vector3 GetInputPoint()
        {
            var point = UnityEngine.Input.mousePosition;
            point.z = -Camera.main.transform.position.z;
            return Camera.main.ScreenToWorldPoint(point);
        }

        /// <summary>
        /// Good for both Mobile & Desktop
        /// </summary>
        bool ShouldFire()
        {
            return UnityEngine.Input.GetMouseButton(0);
        }

        #endregion
    }
}