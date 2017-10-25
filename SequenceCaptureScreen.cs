namespace SequenceCaptureSystem {

    public class SequenceCaptureScreen : AbstractSequenceCapture {

        #region Unity
    	protected virtual void OnPostRender() {
            Capture ();
        }
        #endregion
    }
}
