using UnityEngine;
using System.Collections;

using game.projectile;

using game.unit;

namespace game.weapon {

	public class CWeaponBase : MonoBehaviour {

		/****************************************
		 * Serialized Fields
		 * **************************************/

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

		[SerializeField]
		protected int m_projectileLayer;



		/****************************************
		 * Protected Fields
		 * **************************************/

		protected CUnitBase m_owner;

		protected float m_timeElapsedLastSpawn;

		protected float m_fireReady;

		protected GameObject m_target;


		/****************************************
		 * Properties
		 * **************************************/

		public GameObject Target {
			get { return m_target; }
			set { m_target = value; }
		}


		/****************************************
		 * Monobehaviour Methods
		 * **************************************/

		public void Awake() {
			Init ();
		}

		public void Update() {
			//timers
			m_timeElapsedLastSpawn += Time.deltaTime;
		}

		/****************************************
		 * Public Methods
		 * **************************************/

		public void Init() {
			//should get a projectile layer from the layer manager
		}

		public void Setup(CUnitBase owner) {
			m_owner = owner;
		}

		public virtual void Shoot() {
			if(m_timeElapsedLastSpawn > 1.0f / m_fireRate) {
				SpawnProjectile();
			}
		}

		public virtual CProjectileBase SpawnProjectile() {

			CProjectileBase newProj = GameObject.Instantiate<CProjectileBase>(m_projectilePrefab);

			//place on pos
			//with rotation
			Vector3 spawnPos = transform.position;
			Quaternion spawnRot = transform.rotation;
			if(m_spawnPoint != null) {
				spawnPos = m_spawnPoint.position;
				spawnRot = m_spawnPoint.rotation;
			}
			newProj.transform.position = spawnPos;
			newProj.transform.rotation = spawnRot;

			//set collision
			Physics.IgnoreCollision(newProj.Collider, m_owner.Collider);
			newProj.Collider.gameObject.layer = m_projectileLayer;

			//set target
			Vector3 vecTarget = spawnRot * Vector3.forward; //default velo is forward
			if(Target != null) {
				vecTarget = Target.transform.position - spawnPos;
			} else {
				
			}
			newProj.Setup(Target);

			//overridden on some weapons
			SetProjInitialVelo(newProj, vecTarget);

			m_timeElapsedLastSpawn = 0;

			return newProj;
		}

		protected virtual void SetProjInitialVelo(CProjectileBase proj, Vector3 vecTargetDelta) {
			//simple towards target
			proj.Velocity = vecTargetDelta.normalized * m_muzzleVelocity;
		}


	}


}