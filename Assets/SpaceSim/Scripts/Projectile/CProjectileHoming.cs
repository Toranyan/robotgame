using UnityEngine;
using System.Collections;


namespace ss.projectile {

	public class CProjectileHoming : CProjectileBase {

		[SerializeField]
		protected float m_turnRate;

		[SerializeField]
		protected GameObject m_target;



		public void FixedUpdate() {

			//Turn towards target
			if(m_target == null) {
				return;
			}
			Vector3 vecDelta = m_target.transform.position - transform.position;
			Vector3 vecForward = transform.rotation * Vector3.forward;

			Vector3 vecFinal = Vector3.RotateTowards(vecForward, vecDelta, m_turnRate * Time.fixedDeltaTime, 1);

			Quaternion rotDelta = Quaternion.FromToRotation(vecForward, vecFinal);

			transform.rotation = rotDelta * transform.rotation;

		}

	}

}