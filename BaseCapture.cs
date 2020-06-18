using nobnak.Gist.Extensions.Texture2DExt;
using nobnak.Gist.Loader;
using nobnak.Gist.Scoped;
using SequenceCaptureSystem.Format;
using System.IO;
using UnityEngine;

namespace SequenceCaptureSystem {

    public class BaseCapture : MonoBehaviour {
        public enum FormatEnum { JPEG = 0, PNG }

		[SerializeField]
		protected FolderPath saveFolder = new FolderPath(specialFolder: System.Environment.SpecialFolder.MyPictures);
        [SerializeField]
        protected FormatEnum format;

        protected AbstractTextureSerializer serializer;

        #region Unity
        protected virtual void OnEnable() {
			var folder = saveFolder.Folder;
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
			var width = Screen.width;
			var height = Screen.height;
			var src = default(RenderTexture);

			var c = GetComponent<Camera>();
			if (c != null) {
				width = c.pixelWidth;
				height = c.pixelHeight;
				src = c.targetTexture;
			}

			using (new RenderTextureActivator(src))
				CaptureDirect(width, height);
		}

		public virtual void Capture(params RenderTexture[] srcs) {
			foreach (var src in srcs) {
				using (new RenderTextureActivator(src))
					CaptureDirect(src.width, src.height);
			}
        }

		protected virtual void CaptureDirect(RenderTexture src) {
			var width = src.width;
			var height = src.height;
			using (new RenderTextureActivator(src))
				CaptureDirect(width, height);
		}

		protected virtual void CaptureDirect(int width, int height) {
			using (var tex = new ScopedObject<Texture2D>(
				Texture2DExtension.Create(width, height, TextureFormat.ARGB32, false, false))) {
				tex.Data.ReadPixels(new Rect(0, 0, width, height), 0, 0);
				serializer.Serialize(tex);
			}
		}

		#region static
		private static ScopedPlug<RenderTexture> GetCap(int width, int height) {
			return new ScopedPlug<RenderTexture>(
							RenderTexture.GetTemporary(width, height, 0,
							RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB),
							s => RenderTexture.ReleaseTemporary(s));
		}
		#endregion
	}
}
