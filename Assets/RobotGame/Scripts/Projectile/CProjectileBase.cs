using UnityEngine;
using System.Collections;

using game.unit;

namespace game.projectile {

	public class CProjectileBase : MonoBehaviour {

		/****************************************
		 * Inspector Fields
		 * **************************************/

		[SerializeField]
		protected float m_lifetime = 5;

		[SerializeField]
		protected float m_damage = 1;

		[SerializeField]
		protected float m_acceleration = 0;

		[SerializeField]
		protected float m_maxVelo = 100;


		[SerializeField]
		protected GameObject m_objectOnDestroy;


		[SerializeField]
		protected GameObject m_target;

		/****************************************
		 * Protected Fields
		 * **************************************/

		protected Rigidbody m_rigidbody;
		protected Collider m_collider;


		protected float m_timeAlive = 0;

		/****************************************
		 * Inspector Fields
		 * **************************************/

		public Vector3 Velocity {
			get {
				return m_rigidbody.velocity;
			}
			set {
				m_rigidbody.velocity = value;
			}
		}

		public bool Initialized {
			get;
			set;
		}

		public float Lifetime {
			get { return m_lifetime; }
			set { m_lifetime = value; }
		}

		public float Damage {
			get { return m_damage; }
			set { m_damage = value; }
		}

		public GameObject Target {
			get { return m_target; }
			set { m_target = value; }
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



		//Public Methods

		public void Awake() {
			Init ();
		}

		public void Init() {
			if(Initialized) {
				return;
			}
			if(m_rigidbody == null) {
				m_rigidbody = GetComponent<Rigidbody>();
			}

			Initialized = true;
		}

		public virtual void Setup(GameObject target) {
			Target = target;
		}

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		public void Update () {
			m_timeAlive += Time.deltaTime;
			if(m_timeAlive > Lifetime) {
				Destroy();
			}
		}
		
		public void Destroy() {

			if(m_objectOnDestroy != null) {
				GameObject obj = GameObject.Instantiate(m_objectOnDestroy);
				obj.transform.position = transform.position;
			}

			Destroy(gameObject);
		}


		public void FixedUpdate() {
			//acc
			if(m_acceleration > 0) {
				Vector3 vecAcc = transform.rotation * Vector3.forward * m_acceleration;
				Rigidbody.AddForce(vecAcc, ForceMode.Acceleration);
			}

			if(m_maxVelo > 0 && Rigidbody.velocity.magnitude > m_maxVelo) {
				//Rigidbody.velocity = Rigidbody.velocity.normalized * m_maxVelo;
				//decelerate
				Vector3 vecDec = -Rigidbody.velocity;
				Rigidbody.AddForce(vecDec, ForceMode.Acceleration);
			}
		}


		public void OnCollisionEnter(Collision collision) {
			//check if other thing is destructible
			IssDestructible dest = collision.gameObject.GetComponent<IssDestructible>();

			//deal damage
			if(dest != null) {
				dest.Damage(Damage);
				//immediate destroy
				Destroy ();
			}
		}

	}

}