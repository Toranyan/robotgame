using UnityEngine;
using System.Collections;

using ss.weapon;

namespace ss.unit {

	public class CssRobotBase : CssUnitBase {

		[SerializeField]
		protected float m_boostForce = 1;

		[SerializeField]
		protected CWeaponBase[] m_weapon;

		[SerializeField]
		protected CssTargetingController m_targetingController;

		//shoot command 
		public virtual void Shoot(int idx) {
			if(m_weapon[idx] != null) {
				m_weapon[idx].Shoot();
			}
		}

		public virtual void Boost() {

			Vector3 vecBoost = VecMove * m_boostForce;

			Rigidbody.AddForce(vecBoost, ForceMode.Impulse);

		}

		public virtual void SelectClosestTarget() {

			m_targetingController.SelectClosestTarget();

			foreach(CWeaponBase wep in m_weapon) {
				wep.Target = m_targetingController.CurrentTarget;
			}

		}

		//public IEnumerator CroutinBoost



	}

}