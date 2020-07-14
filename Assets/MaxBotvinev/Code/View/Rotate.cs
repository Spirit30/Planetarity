using UnityEngine;

namespace Game.View
{
    class Rotate : MonoBehaviour
    {
        #region REFERENCES

        [SerializeField] Vector3 speed;
        [SerializeField] Space space;

        #endregion

        #region MONO BEHAVIOUR EVENTS

        void Update()
        {
            transform.Rotate(speed * Time.deltaTime, space);
        }

        #endregion
    }
}