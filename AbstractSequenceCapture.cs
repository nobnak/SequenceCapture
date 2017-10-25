﻿using SequenceCaptureSystem.Format;
using UnityEngine;

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

        protected Texture2D _tex;

        #region Unity
        protected virtual void OnEnable() {
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

            Time.captureFramerate = fps;
            imageCounter = limitImageCount;

            _tex = new Texture2D(4, 4, TextureFormat.ARGB32, false);
        }
        protected virtual void OnDisable() {
            Time.captureFramerate = 0;

            Destroy(_tex);
        }
        #endregion

        #region Static
        public static void CaptureResolution(out int width, out int height) {
            var tex = RenderTexture.active;
            width = (tex == null ? Screen.width : tex.width);
            height = (tex == null ? Screen.height : tex.height);
        }
        #endregion

        protected virtual void DoCapture() {
            int w, h;
            CaptureResolution(out w, out h);
            _tex.Resize(w, h);
            _tex.ReadPixels(new Rect(0f, 0f, w, h), 0, 0);
            serializer.Serialize(_tex);
        }
        protected virtual void Capture() {
            if (imageCounter > 0 && --imageCounter == 0)
                enabled = false;
            DoCapture ();
    	}

        protected virtual string GetFolderPath() {
            return System.Environment.ExpandEnvironmentVariables(saveFolder);
        }
    }
}
