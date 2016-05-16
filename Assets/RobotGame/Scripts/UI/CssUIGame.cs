using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using game.unit;

namespace game.ui {

	public class CssUIGame : MonoBehaviour {

		[SerializeField]
		protected CRobotBase m_playerUnit;

		[SerializeField]
		protected Text m_textConsole;

		protected string m_stringConsole;

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {

			string.Format("{0} m/s", m_playerUnit.Speed);


			m_stringConsole = ""
			+ string.Format("Target : {0}\n", m_playerUnit.Target)
			+ string.Format("{0} m/s\n", m_playerUnit.Speed)
			+ string.Format("Vec Move : {0}", m_playerUnit.VecMove);


			m_textConsole.text = m_stringConsole;

		}
	}


}