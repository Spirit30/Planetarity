using UnityEngine;

namespace Game.View
{
    class SunView : GravityBody
    {
        #region DATA

        [SerializeField] Color color;
        [SerializeField] float radius;

        #endregion

        #region GETTERS

        public Color Color => color;
        public float Radius => radius;

        public override float Gravity => CoreGravity * Radius;

        #endregion
    }
}