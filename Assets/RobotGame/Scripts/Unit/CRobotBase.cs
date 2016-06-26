using UnityEngine;
using System.Collections;

using game.weapon;

namespace game.unit {

	public class CRobotBase : CUnitBase {

		/****************************************
		 * Inspector Fields
		 * **************************************/

		[SerializeField]
		protected float m_boostForce = 1;

		[SerializeField]
		protected CWeaponBase[] m_weapon;

		[SerializeField]
		protected CTargetingController m_targetingController;

		/****************************************
		 * Properties
		 * **************************************/

		public override GameObject Target {
			get { return m_targetingController.CurrentTarget; }
		}

		/****************************************
		 * Public Methods
		 * **************************************/

		public override void Init() {

			base.Init();

			foreach(CWeaponBase wep in m_weapon) {
				wep.Setup(this);
			}

		}

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

		public void SetTargetVec(Vector3 vec) {
			foreach(CWeaponBase wep in m_weapon) {
				wep.TargetPos = vec;
			}
		}


	}

}