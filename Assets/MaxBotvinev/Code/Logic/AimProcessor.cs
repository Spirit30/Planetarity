using UnityEngine;

namespace Game.Logic
{
    static class AimProcessor
    {
		#region GETTERS

		public static Vector3 Calculate(
			Vector3 cannonPosition, 
			float missileSpeed, 
			Vector3 previousTargetPosition, 
			Vector3 targetPosition)
		{
			float dist = Vector3.Distance(cannonPosition, targetPosition);
			float timeToTarget = dist / missileSpeed;
			Vector3 targetSpeed = (targetPosition - previousTargetPosition) / Time.deltaTime;
			return targetPosition + targetSpeed * timeToTarget;
		}

		#endregion
	}
}