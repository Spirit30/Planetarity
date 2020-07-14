using System;
using UnityEngine;

namespace Game.Data
{
    [Serializable]
    class MissileConfig
    {
        #region DATA

        [SerializeField] float acceleration;
        [SerializeField] float weight;
        [SerializeField] float cooldown;

        #endregion

        #region GETTERS

        public float Acceleration => acceleration;
        public float Weight => weight;
        public float Cooldown => cooldown;

        #endregion
    }
}