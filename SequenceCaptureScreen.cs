using System.Collections;
using UnityEngine;

namespace SequenceCaptureSystem {

    public class SequenceCaptureScreen : BaseSequenceCapture {
		protected Coroutine coroutineCapture;

		#region Unity
		protected override void OnEnable() {
			base.OnEnable();
			coroutineCapture = StartCoroutine(ProcessCapture());
		}
		protected override void OnDisable() {
			base.OnDisable();
			if (coroutineCapture != null) {
				StopCoroutine(coroutineCapture);
				coroutineCapture = null;
			}
		}
		#endregion

		protected IEnumerator ProcessCapture() {
			while (true) {
				yield return new WaitForEndOfFrame();
				CapturePerFrame();
			}
		}
	}
}
