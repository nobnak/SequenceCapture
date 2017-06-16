using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;
using SequenceCaptureSystem.Format;

namespace SequenceCaptureSystem {

    public class SequenceCaptureOnRenderImage : AbstractSequenceCapture {
        protected Texture2D _tex;

        protected RenderTexture capturedTex;

        #region Unity
        protected virtual void OnRenderImage(RenderTexture src, RenderTexture dst) {
            capturedTex = RenderTexture.GetTemporary (src.width, src.height);
            Graphics.Blit (src, capturedTex);
            Graphics.Blit (src, dst);
        }
        protected virtual void OnPostRender() {
            if (capturedTex != null) {
                Capture ();
                RenderTexture.ReleaseTemporary (capturedTex);
            }
        }
        #endregion

        #region implemented abstract members of AbstractSequenceCapture
        protected override void Start () {
            _tex = new Texture2D (0, 0, TextureFormat.ARGB32, false);
        }
        protected override void Stop () {
            Destroy (_tex);
        }
        protected override void DoCapture () {
            var prevActive = RenderTexture.active;
            try {
                RenderTexture.active = capturedTex;

                var w = capturedTex.width;
                var h = capturedTex.height;

                _tex.Resize (w, h);
                _tex.ReadPixels (new Rect (0f, 0f, w, h), 0, 0);
                serializer.Serialize(_tex);
            } catch (System.Exception e) {
                Debug.LogError (e);
            } finally {
                RenderTexture.active = prevActive;
            }
        }
        #endregion
    }
}
