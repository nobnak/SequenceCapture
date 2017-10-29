using Gist.Scoped;
using UnityEngine;

namespace SequenceCaptureSystem {

    public class SequenceCaptureOnRenderImage : SequenceCaptureBase {

        #region Unity
        protected virtual void OnRenderImage(RenderTexture src, RenderTexture dst) {
            CapturePerFrame(src);
            Graphics.Blit(src, dst);
        }
        #endregion
    }
}
