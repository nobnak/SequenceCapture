using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;
using SequenceCaptureSystem.Format;

namespace SequenceCaptureSystem {

    public class SequenceCaptureScreen : AbstractSequenceCapture {
        protected Texture2D _tex;

        #region Unity
    	protected virtual void OnPostRender() {
            Capture ();
        }
        #endregion

        #region implemented abstract members of AbstractSequenceCapture
        protected override void Start () {
            _tex = new Texture2D (4, 4, TextureFormat.ARGB32, false);
        }
        protected override void Stop () {
            Destroy (_tex);
        }
        protected override void DoCapture () {
            try {
                var w = Screen.width;
                var h = Screen.height;
                _tex.Resize (w, h);
                _tex.ReadPixels (new Rect (0f, 0f, w, h), 0, 0);
                serializer.Serialize(_tex);
            } catch (System.Exception e) {
                Debug.LogError (e);
            }
        }
        #endregion
    }
}
