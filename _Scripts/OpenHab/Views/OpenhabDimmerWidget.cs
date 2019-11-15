using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace se.Studio13.OpenHabUnity
{
    /// <summary>
    /// Dimmer Widget
    /// ------------
    /// This widget is as it sounds Equal to Dimmer in HabPanel.
    /// A slider that sets value of item. Usually for dimming light or similar.
    /// 
    /// We need the Start() function to remain as is so if you want the
    /// widget to initialize something in start. Use InitWidget().
    /// OnUpdate is called when a StateChange has been made, either via eventbus
    /// or if you for some reason manually GET the item from itemcontroller. (ie. at initialize)
    /// 
    /// @author     A. Stromberg
    /// @company    Studio 13
    /// @email      adde@upperfield.se
    /// </summary>
    public class OpenhabDimmerWidget : MonoBehaviour
    {
        [Header("Item & Server Setup")]
        [Tooltip("Server url with port if needed. ie. http://localhost:8080")]
        public string _Server = "http://openhab:8080";
        [Tooltip("Item name in openhab. ie. gf_Hallway_Light")]
        public string _Item;
        [Tooltip("If you wan't to subscribe to events on this item. What event. Usually StateChanged")]
        public EvtType _SubscriptionType = EvtType.ItemStateChangedEvent;

        [Header("Widget Setup")]
        public Slider _Slider;

        private ItemController _itemController;

        /// <summary>
        /// Initialize ItemController
        /// </summary>
        void Start()
        {
            // Add or get controller component
            if (GetComponent<ItemController>() != null)
            {
                _itemController = GetComponent<ItemController>();
            }
            else
            {
                _itemController = gameObject.AddComponent<ItemController>();
            }

            _itemController.Initialize(_Server, _Item, _SubscriptionType);

            _itemController.updateItem += OnUpdate;
            InitWidget();

        }

        /// <summary>
        /// For public field initialization etc. This is to be able to use
        /// a generic start function for all widgets. This function is called for
        /// at end of Start()
        /// </summary>
        private void InitWidget()
        {
            if (_Slider == null) _Slider = GetComponent<Slider>();
            // Assume dimmer is 0-100 percentage so make slider no 0-1 float but 0-100 int.
            _Slider.wholeNumbers = true;
            _Slider.minValue = 0;
            _Slider.maxValue = 100;
        }

        /// <summary>
        /// When an item updates from server. This function is
        /// called from ItemController when Item is Updated on server.
        /// Begin with a check if Item and UI state is equal. Otherwise we
        /// might get flickering as the state event is sent after update from
        /// UI. This will Sync as long as Event Stream is online.
        /// </summary>
        public void OnUpdate()
        {
            float value = _itemController.GetItemStateAsDimmer();
            //Debug.Log("OnUpdate recieved state: " + value);
            // Failed to parse the dimmer
            if (value == -1 || value > 100)
            {
                _Slider.value = 0f;
                _Slider.interactable = false;
            } else
            {
                _Slider.interactable = true;
                _Slider.value = value;
            }
        }

        /// <summary>
        /// Update item from UI. Call itemcontroller and update Item on server.
        /// If update is true, an event will be recieved. If state is equal no
        /// new UI update is necesarry. If not equal the PUT has failed and we need
        /// to revert UI state to server state.
        /// </summary>
        public void OnSetItem()
        {
            _itemController.SetItemStateAsDimmer((int)_Slider.value);
        }

        /// <summary>
        /// Stop event listening from controller
        /// </summary>
        void OnDisable()
        {
            _itemController.updateItem -= OnUpdate;
        }
    }
}
