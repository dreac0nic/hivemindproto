using UnityEngine;
using System.Collections;

namespace HiveMind
{
	public class Options : MonoBehaviour {
		// Camera Options
		public static int ScrollBorder { get { return 25; }}
		public static float ScrollSpeed { get { return 25; }}
		public static float ZoomSpeed { get { return 40; } }
		public static float ZoomMin { get { return 50; } }
		public static float ZoomMax { get { return 100; } }
		public static Vector3 CameraStartPos { get { return new Vector3(0.0f, 126.0f, 0.0f); }}
		public static Vector3 CameraStartRot { get { return new Vector3(68.0f, 0.0f, 0.0f); }}
	}
}
