
using UnityEngine;
using UnityEngine.UI;

namespace se.Studio13.OpenHabUnity
{
    /// <summary>
    /// Status widget
    /// ------------
    /// This widget looks for the eventbus gameobject and listens to EventbusConnection
    /// event.
    /// 
    /// @author     A. Stromberg
    /// @company    Studio 13
    /// @email      adde@upperfield.se
    /// </summary>
    public class OpenhabEventbusStatusWidget : MonoBehaviour
    {
        public Color _DisconnectedColor;
        public Color _ConnectedColor;
        public Image _StatusImage;

        private EventController _Eventbus;

        /// <summary>
        /// Search for Eventbus gameobject and subscribe to events
        /// </summary>
        void Start()
        {
            _Eventbus = GameObject.FindGameObjectWithTag("Eventbus").GetComponent<EventController>();
            if (_Eventbus != null)
            {
                _Eventbus.connectedEventBus += ConnectionStatus;
            } else
            {
                _StatusImage.color = _DisconnectedColor;
            }
        }

        /// <summary>
        /// Change fill color based on connectionstatus
        /// </summary>
        /// <param name="eventbusConnection"></param>
        private void ConnectionStatus(bool eventbusConnection)
        {
            if(eventbusConnection)
            {
                _StatusImage.color = _ConnectedColor;
            } else
            {
                _StatusImage.color = _DisconnectedColor;
            }
        }

        /// <summary>
        /// Unsubscribe when disabled
        /// </summary>
        private void OnDisable()
        {
            if(_Eventbus != null) _Eventbus.connectedEventBus -= ConnectionStatus;
        }
    }
}

