using UnityEngine;
using System.Collections;


namespace game.unit {

	public class CUnitBase : MonoBehaviour, IDestructible {

		/****************************************
		 * Inspector Fields
		 * **************************************/

		[SerializeField]
		protected float m_thrust = 100;

		[SerializeField]
		protected float m_turnRate = 180;


		protected Vector3 m_vecMove;

		protected Vector3 m_vecRotForce; //rotation command

		/****************************************
		 * Protected Fields
		 * **************************************/

		//cache
		protected Transform m_transform;
		protected Rigidbody m_rigidbody;
		protected Collider m_collider; 


		protected bool m_boosting;

		/****************************************
		 * Properties
		 * **************************************/

		public Transform Transform {
			get {
				if(m_transform == null) {
					m_transform = transform;
				}
				return m_transform;
			}
		}

		public Rigidbody Rigidbody {
			get {
				if(m_rigidbody == null) {
					m_rigidbody = GetComponent<Rigidbody>();
				}
				return m_rigidbody;
			}
		}

		public Collider Collider {
			get {
				if(m_collider == null) {
					m_collider = GetComponent<Collider>();
				}
				return m_collider;
			}
		}


		public Vector3 VecMove {
			get { return m_vecMove; }
			set { m_vecMove = value; }
		}

		public Vector3 VecRotForce {
			get { return m_vecRotForce; }
			set { m_vecRotForce = value; }
		}

		public Vector3 VecTargetRot {
			get;
			set;
		}

		public virtual GameObject Target {
			get;
			set;
		}


		public float Speed {
			get { return m_rigidbody.velocity.magnitude; }
		}

		public GameObject GameObject {
			get { return gameObject; }
		}


		/****************************************
		 * Methods
		 * **************************************/

		// Use this for initialization
		void Awake () {
			Init ();
		}
		
		// Update is called once per frame
		void Update () {
			//update target rotation
			//VecTargetRot = m_vecMove
		
		}

		public virtual void Init() {


		}

		public void Damage(float damage) {

		}

		public void Destroy() {
			Destroy(gameObject);
		}

		protected void FixedUpdate() {
			//movement
			Rigidbody.AddForce(m_vecMove * m_thrust);

			//turn to rot
			/*
			Vector3 vecRot = VecTargetRot - transform.rotation.eulerAngles;
			vecRot.x = 0;
			vecRot.z = 0;
			vecRot.Normalize();
			*/

			//rotate using a direct rotation


			if(VecMove.magnitude > 0.1f) {
				float maxRot = m_turnRate * Time.fixedDeltaTime;
				Transform.rotation = Quaternion.RotateTowards(Transform.rotation, Quaternion.LookRotation(VecMove), maxRot);
			}
			//Vector3 rotFinal = Vector3.RotateTowards(Transform.rotation.eulerAngles, VecTargetRot, maxRadians, 1000);

			//Transform.rotation = Quaternion.Euler(rotFinal);

			//m_vecRotForce = vecRot;

			//m_rigidbody.AddTorque(m_vecRotForce * m_turnRate);

		}

	}

}