using UnityEngine;
using System.Collections;


namespace mt.input {

	public enum eTouchPhase {
		none,
		began,
		down,
		ended,
		num
	}


	public class CmtTouch { //touch wrapper
		
		public int fingerId = -1;
		public eTouchPhase phase = eTouchPhase.none;
		public Vector2 position;
		public Vector2 posStart;
		public Vector2 posEnd;
		public float timeStart;
		public float timeEnd;
		
		public float maxSqDrag;
		
		public void Begin() {
			timeStart = Time.time;
			posStart = position;
			maxSqDrag = 0;
		}
		
		public void Update() {
			float dist = Vector2.SqrMagnitude(position - posStart);
			if(dist > maxSqDrag) {
				maxSqDrag = dist;	
			}
		}
		
		public void End() {
			timeEnd = Time.time;
		}
		
		public float GetTimeDown() {
			return timeEnd - timeStart;
		}
		
		public float GetMaxSqDrag() {
			//Debug.Log(maxSqDrag);
			return maxSqDrag;
		}
		
	}


	public class CmtInput : MonoBehaviour {
		
		/****************************************
		 * Protected Fields
		 * **************************************/

		protected bool m_initialized;

		protected CmtTouch[] m_touch;
		
		//flags
		protected bool m_bUseTouch;


		//Protected Variables
		protected static CmtInput m_instance; //the singleton



		/****************************************
		 * Properties
		 * **************************************/

		public CmtTouch[] Touches {
			get { return m_touch; }
		}

		public static CmtInput Instance {
			get { 
				if(m_instance == null) {
					//search
					m_instance = GameObject.FindObjectOfType<CmtInput>();


					if(m_instance == null) {
						//create
						m_instance = new GameObject("CmtInput Singleton").AddComponent<CmtInput>();

					}

				}

				return m_instance;

			}
		}
		

		/****************************************
		 * Public Methods
		 * **************************************/

		// Use this for initialization
		public void Awake () {
			m_instance = this;
			
			Init();
		}
		public void Init() {
			if(m_initialized) {
				return;
			}
			
			//Debug.Log("Initalizing Input");
			
			//create touch objects
			m_touch = new CmtTouch[5];
			for(int i = 0; i < m_touch.Length; i++) {
				m_touch[i] = new CmtTouch();
			}
			
			//touch screen?
			if(	Application.platform == RuntimePlatform.Android ||
				Application.platform == RuntimePlatform.IPhonePlayer) {
				
				//use touch
				m_bUseTouch = true;
				
				Input.multiTouchEnabled = true;
			} else {
				//use mouse
				m_bUseTouch = false;
			}

			m_initialized = true;

		}
		
		
		public void OnDestroy() {
			m_instance = null;
		}
		
		
		
		// Update is called once per frame
		public void Update () {
			Run();
		}
		public void Run() {
			//process touches
			if(m_bUseTouch) {
				for(int j = 0; j < 5; j++) {
					
					if(j < Input.touches.Length) {
						//store position
						Touch touch = Input.GetTouch(j);
						m_touch[j].position = touch.position;
						m_touch[j].fingerId = touch.fingerId;
						
						switch(touch.phase) {
						case TouchPhase.Began : 
							m_touch[j].phase = eTouchPhase.began;
							m_touch[j].Begin();
							break;
						case TouchPhase.Ended : 
						case TouchPhase.Canceled : 
							m_touch[j].phase = eTouchPhase.ended;
							m_touch[j].End();
							break;
						case TouchPhase.Stationary : 
						case TouchPhase.Moved : 
							m_touch[j].phase = eTouchPhase.down;
							
							m_touch[j].Update();
							
							break;
						default:
							break;
						}
					} else {
						m_touch[j].phase = eTouchPhase.none;
						m_touch[j].fingerId = -1;
					}
				}
				
			} else { //emulate with mouse
				//finger ID
				m_touch[0].position = Input.mousePosition;
				m_touch[0].fingerId = 0;
				
				if(Input.GetMouseButtonDown(0)) { //pressed
					m_touch[0].phase = eTouchPhase.began;
					m_touch[0].Begin();
				} else if(Input.GetMouseButton(0) ){ //held
					m_touch[0].phase = eTouchPhase.down;
					m_touch[0].Update();
				} else if(Input.GetMouseButtonUp(0) ){ //up
					m_touch[0].phase = eTouchPhase.ended;
					m_touch[0].End();
				} else {
					m_touch[0].fingerId = -1;
					m_touch[0].phase = eTouchPhase.none;
				}
				//finger ID
				m_touch[0].position = Input.mousePosition;
			}
			
			
		}
		

	
		
	}

} 
