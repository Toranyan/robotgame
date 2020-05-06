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

		[SerializeField]
		protected bool m_rotateToTarget;

		/****************************************
		 * Properties
		 * **************************************/

		public override GameObject Target {
			get { return m_targetingController.CurrentTarget; }
		}

		/*
		public override Vector3 TargetVec {
			get;
			protected set;
		}*/

		/****************************************
		 * Public Methods
		 * **************************************/

		public override void Init() {
			base.Init();

			foreach(CWeaponBase wep in m_weapon) {
				wep.Setup(this);
			}

			m_targetingController.TargetSelected += OnTargetSelected;
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

		//Targeting
		public virtual void SelectClosestTarget() {
			m_targetingController.SelectClosestTarget();
			UpdateWeaponTarget(m_targetingController.CurrentTarget);
		}

		public override void SetTargetVec(Vector3 vec) {
			TargetVec = vec;
			//Debug.Log(TargetVec);
			foreach(CWeaponBase wep in m_weapon) {
				wep.TargetVec = vec;
			}
		}

		public void UpdateWeaponTarget(GameObject target)
		{
			foreach (CWeaponBase wep in m_weapon)
			{
				wep.Target = target;
			}
		}

		protected override void UpdateRotation()
		{
			if (Target != null && m_rotateToTarget)
			{
				float maxRot = m_turnRate * Time.fixedDeltaTime;

				Vector3 targetVec = Target.transform.position - transform.position;
				targetVec.y = 0;
				targetVec.Normalize();

				Transform.rotation = Quaternion.RotateTowards(Transform.rotation, Quaternion.LookRotation(targetVec), maxRot);
			} else
			{
				//rotate towards movement
				base.UpdateRotation();
			}


		}
		protected void OnTargetSelected(GameObject target)
		{
			UpdateWeaponTarget(target);
		}


	}

}