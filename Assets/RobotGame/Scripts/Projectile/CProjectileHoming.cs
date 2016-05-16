using UnityEngine;
using System.Collections;


namespace game.projectile {

	public class CProjectileHoming : CProjectileBase {

		[SerializeField]
		protected float m_turnRate;

		[SerializeField]
		protected Vector3 m_vecTarget;

		[SerializeField]
		protected bool m_homing; //attempt to home in even if no target

		public bool Homing {
			get { return m_homing; }
			set { m_homing = value; }
		}


		public override void Setup(GameObject target) {
			base.Setup(target);
			if(target != null) {
				Target = target;
				m_homing = true;
			}
		}

		public void FixedUpdate() {

			base.FixedUpdate();

			if(!m_homing) {
				return;
			}

			if(m_target != null) {
				m_vecTarget = m_target.transform.position;
			} 

			//Turn towards target
			Vector3 vecDelta = m_vecTarget - transform.position;
			Vector3 vecForward = transform.rotation * Vector3.forward;

			Vector3 vecFinal = Vector3.RotateTowards(vecForward, vecDelta, m_turnRate * Mathf.Deg2Rad * Time.fixedDeltaTime, 1);
			//Vector3 vecFinal = Vector3.RotateTowards(vecForward, vecDelta, 100, 1);

			Quaternion rotDelta = Quaternion.FromToRotation(vecForward, vecFinal);

			transform.rotation = rotDelta * transform.rotation;

		}




	}

}