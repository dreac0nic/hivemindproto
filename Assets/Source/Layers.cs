using UnityEngine;
using System.Collections;

namespace HiveMind
{
	public class Layers : MonoBehaviour {
		public static int Default		= 1 << 0;
		public static int TransparentFX	= 1 << 1;
		public static int IgnoreRaycast	= 1 << 2;
		public static int Water			= 1 << 4;
		public static int UI			= 1 << 5;
		public static int Map			= 1 << 8;
	}
}