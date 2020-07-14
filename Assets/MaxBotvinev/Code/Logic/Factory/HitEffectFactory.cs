using Game.View;
using UnityEngine;

namespace Game.Logic.Factory
{
    class HitEffectFactory : Factory
    {
        #region GETTERS

        public HitEffect GetHitEffect(Vector3 point, Planet planet)
        {
            return GetHitEffect(point, planet.View.Color);
        }

        public HitEffect GetHitEffect(Vector3 point, SunView sun)
        {
            return GetHitEffect(point, sun.Color);
        }

        public HitEffect GetHitEffect(Vector3 point, Color color)
        {
            HitEffect hitEffect = (HitEffect)Get(point);
            hitEffect.Color = color;
            return hitEffect;
        }

        #endregion
    }
}