using UnityEngine;
using System.Collections;

using System.Collections.Generic;

namespace mt.state {

	public class CmtStateQueue {

		public List<ImtState> m_listState = new List<ImtState>();




		public virtual void Run() {

			if(m_listState.Count > 0) {
				//remove ended states
				while(m_listState[0].IsEnd) {
					m_listState.RemoveAt(0);
				}
			}

			if(m_listState.Count > 0) {
				m_listState[0].Run();
			}

		}

		public virtual void PushState(ImtState state) {
			m_listState.Insert(0, state);
		}

		public virtual void AddState(ImtState state) {
			m_listState.Add(state);
		}

	}

}