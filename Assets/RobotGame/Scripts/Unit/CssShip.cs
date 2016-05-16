using UnityEngine;
using System.Collections;

namespace game.unit {

	public class CssShip : CUnitBase {


		[SerializeField]
		protected float m_torque;

		//protected Transform m_transform;

		//protected Rigidbody m_rigidbody;

		//protected Vector3 m_vecMove; 

		// Use this for initialization
		void Start () {

			m_transform = transform;
			m_rigidbody = GetComponent<Rigidbody>();
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}



		public new void FixedUpdate() {

			//rotation

			Vector3 vecTorque = Vector3.zero;
			//swap axes
			vecTorque.x = -m_vecMove.y;
			vecTorque.y = m_vecMove.x;
			vecTorque = vecTorque * m_torque;
			m_rigidbody.AddRelativeTorque(vecTorque, ForceMode.Force);


			//forward motion
			Vector3 vecThrust = transform.rotation * Vector3.forward * m_thrust;

			m_rigidbody.AddForce(vecThrust, ForceMode.Force);

		}



	}

}
