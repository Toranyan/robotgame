using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using ss.unit;

namespace ss.ui {

	public class CssUIGame : MonoBehaviour {

		[SerializeField]
		protected CssUnitBase m_playerUnit;

		[SerializeField]
		protected Text m_textSpeed;

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {

			m_textSpeed.text = string.Format("{0} m/s", m_playerUnit.Speed);

		}
	}


}