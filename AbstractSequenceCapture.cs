using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;
using SequenceCaptureSystem.Format;

namespace SequenceCaptureSystem {

    [RequireComponent(typeof(Camera))]
    public abstract class AbstractSequenceCapture : MonoBehaviour {
        public enum FormatEnum { JPEG = 0, PNG }

        [SerializeField]
        protected string saveFolder = @"%USERPROFILE%\Pictures";
        [SerializeField]
        protected int fps = 30;
        [SerializeField]
        protected int limitImageCount = -1;
        [SerializeField]
        protected FormatEnum format;

        [SerializeField]
        protected int imageCounter = 0;
        protected AbstractTextureSerializer serializer;

        #region Unity
        protected virtual void Awake() {
            var folder = GetFolderPath();
            Debug.LogFormat ("Folder Path {0}", folder);
            switch (format) {
            case FormatEnum.JPEG:
                serializer = new JpegSerializer (folder);
                break;
            default:
                serializer = new PngSerializer (folder);
                break;
            }

            Start();
        }
        protected virtual void OnEnable() {
            Time.captureFramerate = fps;
            imageCounter = limitImageCount;
        }
        protected virtual void OnDisable() {
            Time.captureFramerate = 0;
        }
        protected virtual void OnDestroy() {
            Stop();
        }
        #endregion

        protected abstract void Start();
        protected abstract void DoCapture();
        protected abstract void Stop();

        protected virtual void Capture() {
            if (imageCounter > 0 && --imageCounter == 0)
                enabled = false;

            DoCapture ();
    	}
        protected virtual string GetFolderPath() {
            return System.Environment.ExpandEnvironmentVariables (saveFolder);
        }
    }
}
