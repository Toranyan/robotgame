using UnityEngine;
using System.Collections;

using mt.ui;

namespace mt.input {

public class CmtPadAdapter : MonoBehaviour {


		[SerializeField]
		protected Camera m_camera;

		[SerializeField]
		protected CmtUIPad m_uiPad;

		// Update is called once per frame
		void Update () {
			if(m_camera == null) {
				return;
			}

			Vector3 vecInput = Vector3.up; //0,1,0

			Vector3 vecWorld = m_camera.transform.rotation * vecInput;
			//Vector3 vecWorld = m_camera.ScreenToWorldPoint(vecInput);

			Debug.Log(vecWorld);

		
		}


	}


}
