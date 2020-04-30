﻿using UnityEngine;
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

		[SerializeField]
		protected bool m_aimEnabled; //aim at the target



		/****************************************
		 * Protected Fields
		 * **************************************/

		protected CUnitBase m_owner;

		protected float m_timeElapsedLastSpawn;

		protected float m_fireReady;

		protected GameObject m_target;
		protected Vector3 m_targetVec;


		/****************************************
		 * Properties
		 * **************************************/

		public GameObject Target {
			get { return m_target; }
			set { m_target = value; }
		}

		public Vector3 TargetVec {
			get { return m_targetVec; }
			set { m_targetVec = value; }
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

			//set collision
			Physics.IgnoreCollision(newProj.Collider, m_owner.Collider);
			newProj.Collider.gameObject.layer = m_projectileLayer;

			//place on pos
			Vector3 spawnPos = transform.position;
			if(m_spawnPoint != null) {
				spawnPos = m_spawnPoint.position;
			}
			newProj.transform.position = spawnPos;


			//set target vector
			Vector3 vecTargetDelta = transform.rotation * Vector3.forward; //default velo is forward
			if(m_aimEnabled) {
				if(Target != null) { //target object
					vecTargetDelta = Target.transform.position - spawnPos;
				} else {
					//target vector
					//vecTargetDelta = m_targetVec - transform.position;
					vecTargetDelta = m_spawnPoint.transform.rotation * Vector3.forward;
				}
			} else {
				//use default
				if(m_spawnPoint != null) {
					vecTargetDelta = m_spawnPoint.transform.rotation * Vector3.forward;
				}
			}


			//rotation



			//set projectile target
			newProj.SetTarget(Target);

			//overridden on some weapons
			SetProjInitialVelo(newProj, vecTargetDelta);

			m_timeElapsedLastSpawn = 0;

			return newProj;
		}

		/****************************************
		 * Protected Methods
		 * **************************************/

		protected virtual void SetProjInitialVelo(CProjectileBase proj, Vector3 vecTargetDelta) {
			//simple towards target
			proj.Velocity = vecTargetDelta.normalized * m_muzzleVelocity;

			//also alter rotation
			proj.transform.rotation = Quaternion.LookRotation(vecTargetDelta);

		}


	}


}