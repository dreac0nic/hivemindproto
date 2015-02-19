using UnityEngine;
using System.Collections;

namespace HiveMind
{
	public class Options : MonoBehaviour {
		private static Options options;
		public static Options O {
			get {
				return options;
			}
		}

		// Camera Options
		public int ScrollBorder = 25;
		public float ScrollSpeed = 25;
		public float ZoomSpeed = 40;
		public float ZoomMin = 50;
		public float ZoomMax = 100;
		public Vector3 CameraStartPos = new Vector3(0.0f, 126.0f, 0.0f);
		public Vector3 CameraStartRot = new Vector3(68.0f, 0.0f, 0.0f);
		public Camera MainCamera;
		public Camera UICamera;
		public Camera UnitCamera;
		public Camera OverlayCamera;

		// Unit Options
		public Material FeralColor;
		public float FeralMinConvertPower = 0f;
		public GameObject SelectMarker;
		public GameObject UnitUI;

		void Start()
		{
			if(!options)
				options = this;
		}
	}
}
