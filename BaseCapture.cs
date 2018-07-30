using nobnak.Gist.Extensions.Texture2DExt;
using nobnak.Gist.Scoped;
using SequenceCaptureSystem.Format;
using System.IO;
using UnityEngine;

namespace SequenceCaptureSystem {

    public class BaseCapture : MonoBehaviour {
        public enum FormatEnum { JPEG = 0, PNG }

        [SerializeField]
        protected string saveFolder = @"%USERPROFILE%\Pictures";
        [SerializeField]
        protected FormatEnum format;

        protected AbstractTextureSerializer serializer;

        #region Unity
        protected virtual void OnEnable() {
            var folder = GetFolderPath();
			Directory.CreateDirectory(folder);
            Debug.LogFormat ("Folder Path {0}", folder);
            switch (format) {
            case FormatEnum.JPEG:
                serializer = new JpegSerializer (folder);
                break;
            default:
                serializer = new PngSerializer (folder);
                break;
            }
        }
        protected virtual void OnDisable() {
        }
        #endregion

        #region Static
        public static void CaptureResolution(out int width, out int height) {
            var tex = RenderTexture.active;
            width = (tex == null ? Screen.width : tex.width);
            height = (tex == null ? Screen.height : tex.height);
        }
        #endregion

        public virtual void Capture() {
			using (var cap = new ScopedPlug<RenderTexture>(
				RenderTexture.GetTemporary(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB),
				s => RenderTexture.ReleaseTemporary(s))) {

				Graphics.Blit(null, cap.Data);
				CaptureDirect(cap.Data);
			}
        }
        public virtual void Capture(params RenderTexture[] srcs) {
			foreach (var src in srcs) {
				using (var cap = new ScopedPlug<RenderTexture>(
					RenderTexture.GetTemporary(src.width, src.height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB),
					s => RenderTexture.ReleaseTemporary(s))) {

					Graphics.Blit(src, cap.Data);
					CaptureDirect(cap.Data);
				}
			}
        }

		protected virtual void CaptureDirect(RenderTexture src) {
			using (var tex = new ScopedObject<Texture2D>(
				Texture2DExtension.Create(src.width, src.height, TextureFormat.ARGB32, false, false)))
			using (new RenderTextureActivator(src)) {
				tex.Data.ReadPixels(new Rect(0, 0, src.width, src.height), 0, 0);
				serializer.Serialize(tex);
			}
		}

		protected virtual string GetFolderPath() {
            return System.Environment.ExpandEnvironmentVariables(saveFolder);
        }
    }
}
