using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;
using SequenceCaptureSystem.Format;
using Gist.Scoped;

namespace SequenceCaptureSystem {

    public class SequenceCaptureOnRenderImage : AbstractSequenceCapture {

        #region Unity
        protected virtual void OnRenderImage(RenderTexture src, RenderTexture dst) {
#if false
            var capturedTex = RenderTexture.GetTemporary (src.width, src.height);
            Graphics.Blit (src, capturedTex);
            using (new ScopedRenderTextureActivator(capturedTex))
                Capture();
            RenderTexture.ReleaseTemporary(capturedTex);
#else
            using (new ScopedRenderTextureActivator(src))
                Capture();
#endif

            Graphics.Blit(src, dst);
        }
#endregion
    }
}
