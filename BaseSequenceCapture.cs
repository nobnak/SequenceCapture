using nobnak.Gist.Scoped;
using SequenceCaptureSystem.Format;
using UnityEngine;

namespace SequenceCaptureSystem {
    
    public class BaseSequenceCapture : BaseCapture {
        [SerializeField]
        protected int fps = 30;
        [SerializeField]
        protected int limitImageCount = -1;
        [SerializeField]
        protected int imageCounter = 0;

        #region Unity
        protected override void OnEnable() {
            base.OnEnable();
            Time.captureFramerate = fps;
            imageCounter = limitImageCount;
        }
        protected override void OnDisable() {
            Time.captureFramerate = 0;
            base.OnDisable();
        }
        #endregion
        
        protected virtual void CapturePerFrame(RenderTexture src = null) {
            if (imageCounter > 0 && --imageCounter == 0)
                enabled = false;

            if (src != null) Capture(src); else Capture();
    	}
    }
}
