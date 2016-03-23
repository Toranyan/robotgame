using UnityEngine;
using System.Collections;

using System;


namespace mt.state {

	public class CmtState : ImtState {

		/****************************************
		 * Properties
		 * **************************************/

		public bool IsEnd {
			get;
			set;
		}

		public bool IsStart {
			get;
			set;
		}


		/****************************************
		 * Methods
		 * **************************************/

		public virtual void Start () {
		
		}

		public virtual void End() {

		}

		public virtual void Run() {

		}

	}



	public interface ImtState {

		bool IsEnd {
			get;
			set;
		}

		bool IsStart {
			get;
			set;
		}

		void Run();
		void Start();
		void End();

	}

}