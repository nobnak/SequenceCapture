using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;

namespace SequenceCaptureSystem.Format {

    public abstract class AbstractTextureSerializer : System.IDisposable {
        const string FORMAT_FILE = "{0}_{1}_{{1}}_{{0:D5}}.{2}";
        protected readonly string formatPath;

        protected int uniquenessCounter = 0;

        public AbstractTextureSerializer(string folder, string extension) {
            formatPath = Path.Combine(folder, string.Format(FORMAT_FILE, 
                SceneManager.GetActiveScene().name,
                System.DateTime.Now.ToString("yyyyMMddHHmmss"),
                extension));
        }
        public virtual bool Serialize(Texture2D tex) {
            try {
                var path = GetUniquePath();
                File.WriteAllBytes(path, ToByte(tex));
                return true;
            } catch (System.Exception e) {
                Debug.LogError(e);
            }
            return false;
        }
        private string GetPath(int id) {
            return string.Format(formatPath, Time.frameCount, id);
        }
        private string GetUniquePath() {
            var path = GetPath(0);
            if (!File.Exists(path)) {
                uniquenessCounter = 0;
                return path;
            }
            while (File.Exists(path = GetPath(++uniquenessCounter))) ;
            return path;
        }

        public abstract byte[] ToByte(Texture2D tex);

        #region IDisposable implementation
        public abstract void Dispose ();
        #endregion
    }
}
