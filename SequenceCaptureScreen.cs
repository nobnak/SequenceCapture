namespace SequenceCaptureSystem {

    public class SequenceCaptureScreen : BaseSequenceCapture {

        #region Unity
    	protected virtual void OnPostRender() {
            CapturePerFrame();
        }
        #endregion
    }
}
