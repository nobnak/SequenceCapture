using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;

namespace SequenceCaptureSystem.Format {

    public class PngSerializer : AbstractTextureSerializer {
        const string EXTENSION = "png";

        public PngSerializer(string folder) : base(folder, EXTENSION) {}

        #region TextureSerializer implementation
        public override byte[] ToByte(Texture2D tex) {
            return tex.EncodeToPNG();
        }
        #endregion

        #region IDisposable implementation
        public override void Dispose () { }
        #endregion
    }
}
