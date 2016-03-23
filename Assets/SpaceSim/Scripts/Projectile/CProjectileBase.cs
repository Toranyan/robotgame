using UnityEngine;
using System.Collections;

using ss.unit;

namespace ss.projectile {

	public class CProjectileBase : MonoBehaviour {

		//Serialized Members

		[SerializeField]
		protected float m_lifetime = 5;

		[SerializeField]
		protected float m_damage = 1;

		[SerializeField]
		protected GameObject m_objectOnDestroy;

		//Protected Members

		protected Rigidbody m_rigidbody;

		protected float m_timeAlive = 0;

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
			get;
			set;
		}


		//Public Methods

		public void Awake() {
			Init ();
		}

		public void Init() {
			if(Initialized) {
				return;
			}
			m_rigidbody = GetComponent<Rigidbody>();
			Initialized = true;
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

			GameObject obj = GameObject.Instantiate(m_objectOnDestroy);
			obj.transform.position = transform.position;

			Destroy(gameObject);
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