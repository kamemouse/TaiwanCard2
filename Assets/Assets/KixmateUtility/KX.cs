using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Net.Kixmate.KixUtility{
	public class KX {
		const string LOG_HEADER = "[KX]";

		[System.Diagnostics.Conditional("DEBUG")]
		public static void D(bool debugEnabled = true, string format = "{0}", params object[] args){
			if (debugEnabled) {
				Debug.LogFormat (LOG_HEADER + format, args);
			}
		}
	}
}