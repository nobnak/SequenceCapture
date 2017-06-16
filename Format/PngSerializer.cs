using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;

namespace SequenceCaptureSystem.Format {

    public class PngSerializer : AbstractTextureSerializer {
        const string EXTENSION = "png";

        public PngSerializer(string folder) : base(folder, EXTENSION) {}

        #region TextureSerializer implementation
        public override bool Serialize (Texture2D tex) {
            try {
                var path = string.Format(formatPath, Time.frameCount);
                File.WriteAllBytes (path, tex.EncodeToPNG ());
                return true;
            } catch (System.Exception e) {
                Debug.LogError (e);
            }
            return false;
        }
        #endregion
        #region IDisposable implementation
        public override void Dispose () { }
        #endregion
    }
}
