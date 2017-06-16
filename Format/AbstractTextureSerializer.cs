using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;

namespace SequenceCaptureSystem.Format {

    public abstract class AbstractTextureSerializer : System.IDisposable {
        const string FORMAT_FILE = "{0}_{1}_{{0:D5}}.{2}";
        protected readonly string formatPath;

        public AbstractTextureSerializer(string folder, string extension) {
            formatPath = Path.Combine(folder, string.Format(FORMAT_FILE, 
                SceneManager.GetActiveScene().name,
                System.DateTime.Now.ToString("yyyyMMddHHmmss"),
                extension));
        }
        public abstract bool Serialize (Texture2D tex);

        #region IDisposable implementation
        public abstract void Dispose ();
        #endregion
    }
}
