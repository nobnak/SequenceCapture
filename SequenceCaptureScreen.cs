namespace SequenceCaptureSystem {

    public class SequenceCaptureScreen : SequenceCaptureBase {

        #region Unity
    	protected virtual void OnPostRender() {
            CapturePerFrame();
        }
        #endregion
    }
}
