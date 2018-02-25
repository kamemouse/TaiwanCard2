using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Net.Kixmate.KixUtility
{
	public class KX: MonoBehaviour
	{
		const string LOG_HEADER = "[KX]";

		#region field

		static readonly KX self;

		#endregion

		#region constractor

		static KX ()
		{
			GameObject o = new GameObject ("KX");
			self = o.AddComponent<KX> ();
			GameObject.DontDestroyOnLoad (o);
		}

		#endregion

		#region public

		[System.Diagnostics.Conditional ("DEBUG")]
		public static void D (bool debugEnabled = true, string format = "{0}", params object[] args)
		{
			if (debugEnabled) {
				Debug.LogFormat (LOG_HEADER + format, args);
			}
		}

		public static Coroutine Delay (float waitTime, Action action)
		{
			return self.StartCoroutine (DelayMethod (waitTime, action));
		}

		public static void Stop (Coroutine coroutine)
		{
			self.StopCoroutine (coroutine);
		}

		#endregion

		#region private

		public static IEnumerator DelayMethod (float waitTime, Action action)
		{
			yield return new WaitForSeconds (waitTime);
			action ();
		}

		#endregion
	}
}