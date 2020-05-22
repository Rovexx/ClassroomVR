using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;

namespace Com.Roel.ClassroomVR
{
    public class SchoolboardManager : MonoBehaviour, Photon.Pun.IPunObservable
    {
        public int activeSlide = 0;
        [Tooltip("A list of images to use as slides")]
        public Texture[] slides;
        private bool _schoolboardActive = false;
        private RawImage _image = null;
        

        // Start is called before the first frame update
        void Start()
        {
            _image = this.gameObject.GetComponent<RawImage>();
            _schoolboardActive = _image.enabled;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
            if (stream.IsWriting) {
                // Send schoolboard status to everyone
                stream.SendNext(activeSlide);
                stream.SendNext(_schoolboardActive);
            } else if (stream.IsReading) {
                UpdateSlideFromNetwork((int)stream.ReceiveNext());
                SyncSchoolBoardFromNetwork((bool)stream.ReceiveNext());
            }
        }

        public void UpdateSlideFromNetwork(int activeSlideRecieved) {
            Debug.Log("LOGGING: Recieved presentation page");
            if (activeSlideRecieved != activeSlide) {
                activeSlide = activeSlideRecieved;
                _image.texture = slides[activeSlide];
            }
        }

        private void SyncSchoolBoardFromNetwork(bool state) {
            Debug.Log("LOGGING: Recieved presentation state");
            if (state != _schoolboardActive) {
                // only 2 possible states so call function instead of first setting the value to the recieved value
                TogglePresentation();
            }
        }

        public void NextSlide() {
            if (activeSlide < slides.Length - 1) {
                activeSlide ++;
                _image.texture = slides[activeSlide];
            } else {
                Debug.Log("LOGGING: Presentation over");
                return;
            }
        }

        public void PreviousSlide() {
            if (activeSlide > 0) {
                activeSlide --;
            }
            _image.texture = slides[activeSlide];
        }

        public void TogglePresentation() {
            if (_schoolboardActive) {
                // Turn off presentation
                _schoolboardActive = false;
            } else {
                // Turn on presentation from first page
                activeSlide = 0;
                _image.texture = slides[activeSlide];
                _schoolboardActive = true;
            }
            _image.enabled = _schoolboardActive;
        }

    }

}
