using UnityEngine;
using System.Collections;

using ss.unit;

namespace ss.controllers {

	public class CssUnitController : MonoBehaviour {

		[SerializeField]
		protected KeyCode m_forwardKey = KeyCode.W;

		[SerializeField]
		protected KeyCode m_backKey = KeyCode.S;

		[SerializeField]
		protected KeyCode m_strafeLeftKey = KeyCode.A;

		[SerializeField]
		protected KeyCode m_strafeRightKey = KeyCode.D;


		[SerializeField]
		protected KeyCode m_leftKey = KeyCode.LeftArrow;

		[SerializeField]
		protected KeyCode m_rightKey = KeyCode.RightArrow;

		[SerializeField]
		protected KeyCode m_shootRightKey = KeyCode.RightControl;

		[SerializeField]
		protected KeyCode m_shootLeftKey = KeyCode.LeftControl;

		[SerializeField]
		protected KeyCode m_boostKey = KeyCode.Space;

		[SerializeField]
		protected KeyCode m_selectTargetKey = KeyCode.Q;

		[SerializeField]
		protected Camera m_playerCamera;



		[SerializeField]
		protected CssRobotBase m_unit;

		// Use this for initialization
		void Start () {

			if(m_playerCamera == null) {
				m_playerCamera = Camera.main;
			}
		
		}
		
		// Update is called once per frame
		void Update () {

			Vector3 vecMove = Vector3.zero;

			if(Input.GetKey(m_forwardKey)) {
				vecMove.z += 1;
			} 
			if(Input.GetKey(m_backKey)) {
				vecMove.z -= 1;
			}
			if(Input.GetKey (m_strafeLeftKey)) {
				vecMove.x -= 1;
			}
			if(Input.GetKey (m_strafeRightKey)) {
				vecMove.x += 1;
			}

			//rotate vector to be relative to camera
			vecMove = m_playerCamera.transform.rotation * vecMove;

			//no y 
			vecMove.y = 0;
			vecMove.Normalize();

			m_unit.VecMove = vecMove;

			//lmb
			if(Input.GetMouseButton(0) || Input.GetKey(m_shootLeftKey)) {
				m_unit.Shoot(0);
			}
			//rmb
			if(Input.GetMouseButton(1) || Input.GetKey(m_shootRightKey)) {
				m_unit.Shoot(1);
			}

			if(Input.GetKey(m_boostKey)) {
				m_unit.Boost();
			}

			if(Input.GetKey(m_selectTargetKey)) {
				m_unit.SelectClosestTarget();
			}

			//rotate the player to the camera
			//Quaternion rotCamera = Quaternion.RotateTowards(m_unit.transform.rotation, m_playerCamera.transform.rotation, 180);
			//Vector3 vecRotCamera = rotCamera.eulerAngles;
			//Vector3 vecRotCamera = m_playerCamera.transform.rotation.eulerAngles - m_unit.transform.rotation.eulerAngles;
			//vecRotCamera.x = 0;
			//vecRotCamera.z = 0;
			//m_unit.VecTargetRot = m_playerCamera.transform.rotation.eulerAngles;
		
		}
	}


}