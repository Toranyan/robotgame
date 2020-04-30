using UnityEngine;
using System.Collections;

using tora.singleton;

namespace game.ui {

	/// <summary>
	/// Game UI
	/// all game ui elements should be accessible from here
	/// </summary>
	public class CGameUI : CSingleton<CGameUI> {

		[SerializeField]
		protected CTargetingUI m_targetingUI;

		public CTargetingUI TargetingUI {
			get { return m_targetingUI; }
		}

		
	}


}

