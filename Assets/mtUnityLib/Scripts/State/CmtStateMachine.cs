using UnityEngine;
using System.Collections;

using System.Collections.Generic;

namespace mt.state {

	public class CmtStateMachine : ImtStateMachine {

		/****************************************
		 * Fields
		 * **************************************/

		protected List<ImtState> m_listState;


		protected ImtState m_currentState;

		/****************************************
		 * Properties
		 * **************************************/

		public ImtState CurrentState {
			get { return m_currentState; }
			protected set { m_currentState = value; }
		}

		/****************************************
		 * Public Interface
		 * **************************************/

		public virtual void Run() {
			
			if(m_currentState != null) {
				m_currentState.Run();
			}

		}

		public void SwitchState(ImtState state) {

			if(CurrentState == state) {
				return;
			}
			
			if(CurrentState != null) {
				CurrentState.End();
			}

			CurrentState = state;

			if(CurrentState != null) {
				CurrentState.Start();
			}
			
		}


		/****************************************
		 * 
		 * **************************************/



	}


	public interface ImtStateMachine {

		void Run();
		void SwitchState(ImtState state);


	}


}