using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;
using SequenceCaptureSystem.Format;

namespace SequenceCaptureSystem {

    public class SequenceCaptureScreen : AbstractSequenceCapture {

        #region Unity
    	protected virtual void OnPostRender() {
            Capture ();
        }
        #endregion
    }
}
