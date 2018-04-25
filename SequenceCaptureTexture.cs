using System.Collections;
using UnityEngine;

namespace SequenceCaptureSystem {

    public class SequenceCaptureTexture : BaseSequenceCapture {
		protected RenderTexture targetTexture;
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

		#region public
		public void SetTexture(RenderTexture tex) {
			this.targetTexture = tex;
		}
		#endregion

		protected IEnumerator ProcessCapture() {
			while (true) {
				yield return new WaitForEndOfFrame();
				CapturePerFrame(targetTexture);
			}
		}
	}
}
