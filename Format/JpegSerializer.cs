using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;

namespace SequenceCaptureSystem.Format {

    public class JpegSerializer : AbstractTextureSerializer {
        const string EXTENSION = "jpg";

        public JpegSerializer(string folder) : base(folder, EXTENSION) {}

        #region TextureSerializer implementation
        public override byte[] ToByte(Texture2D tex) {
            return tex.EncodeToJPG();
        }

        #endregion
        #region IDisposable implementation
        public override void Dispose () { }
        #endregion
    }

}
