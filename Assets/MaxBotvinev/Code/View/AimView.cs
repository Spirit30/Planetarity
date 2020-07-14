using Game.Logic;
using UnityEngine;

namespace Game.View
{
    class AimView : MonoBehaviour
    {
        #region REFERENCES

        [SerializeField] Planet planet;
        [SerializeField] LineRenderer line;

        #endregion

        #region UNITY EVENTS

        void Update()
        {
            line.SetPosition(0, planet.transform.position);
            line.SetPosition(1, transform.position);
        }

        #endregion

        #region INTERFACE

        public void SetPosition(Vector3 point)
        {
            transform.position = point;
        }

        #endregion
    }
}