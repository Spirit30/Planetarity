using UnityEngine;

namespace Game.View
{
    abstract class GravityBody : MonoBehaviour
    {
        #region DATA

        [SerializeField] float coreGravity;

        #endregion

        #region GETTERS

        protected float CoreGravity => coreGravity;
        public abstract float Gravity { get; }

        #endregion
    }
}