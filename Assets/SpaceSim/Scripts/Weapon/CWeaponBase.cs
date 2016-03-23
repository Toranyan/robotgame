using UnityEngine;
using System.Collections;

using ss.projectile;

using ss.unit;

namespace ss.weapon {

	public class CWeaponBase : MonoBehaviour {

		[SerializeField]
		protected CProjectileBase m_projectilePrefab;

		[SerializeField]
		protected Transform m_spawnPoint;


		[SerializeField]
		protected float m_fireRate = 1;

		[SerializeField]
		protected float m_fireDelay;

		[SerializeField]
		protected float m_muzzleVelocity; //velocity of the projectile when it leaves

		[SerializeField]
		protected bool m_automatic;

		protected float m_timeLastFire;

		protected float m_fireReady;

		protected GameObject m_target;

		public GameObject Target {
			get { return m_target; }
			set { m_target = value; }
		}

		public virtual void Shoot() {
			SpawnProjectile();
		}

		public virtual void SpawnProjectile() {

			CProjectileBase newProj = GameObject.Instantiate<CProjectileBase>(m_projectilePrefab);

			//place on pos
			//with rotation
			Vector3 spawnPos = transform.position;
			Quaternion rot = transform.rotation;
			if(m_spawnPoint != null) {
				spawnPos = m_spawnPoint.position;
				rot = m_spawnPoint.rotation;
			}
			newProj.transform.position = spawnPos;
			newProj.transform.rotation = rot;

			Vector3 vecTarget = transform.rotation * Vector3.forward;


			if(Target != null) {
				vecTarget = Target.transform.position - spawnPos;
				vecTarget.Normalize();

				newProj.Target = Target;
			}

			newProj.Velocity = vecTarget * m_muzzleVelocity;

		}



	}


}