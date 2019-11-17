﻿using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace se.Studio13.OpenHabUnity
{
    /// <summary>
    /// Dummy Widget
    /// ------------
    /// This widget is as it sounds Equal to Dummy in HabPanel.
    /// Simple text presented on screen. This is a widget in it's simpliest
    /// form and I use this as base for all other widgets.
    /// 
    /// We need the Start() function to remain as is so if you wan't the
    /// widget to initialize something in start. Use InitWidget().
    /// OnUpdate is called when a StateChange has been made, either via eventbus
    /// or if you for some reason manually GET the item from itemcontroller. (ie. at initialize)
    /// 
    /// @author     A. Stromberg
    /// @company    Studio 13
    /// @email      adde@upperfield.se
    /// </summary>
    public class OpenhabDummyWidget : MonoBehaviour
    {
        [Header("Item & Server Setup")]
        [Tooltip("Server url with port if needed. ie. http://localhost:8080")]
        public string _Server = "http://openhab:8080";
        [Tooltip("Item name in openhab. ie. gf_Hallway_Light")]
        public string _Item;
        [Tooltip("If you wan't to subscribe to events on this item. What event. Usually StateChanged")]
        public EvtType _SubscriptionType = EvtType.ItemStateChangedEvent;

        [Header("Widget Setup")]
        public Text _dummyText;
        public bool _isNumber = true;
        public string _numberFormat = "0.00";
        public string _culture = "en-GB";
        public string _preText = "";
        public string _postText = "";

        private ItemController _itemController;

        /// <summary>
        /// Initialize ItemController
        /// </summary>
        void Start()
        {
            // Add or get controller component
            if(GetComponent<ItemController>() != null)
            {
                _itemController = GetComponent<ItemController>();
            } else
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
            if(_dummyText == null)
            {
                _dummyText = GetComponent<Text>();
            }
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
            string txt = _itemController.GetItemStateAsString();
            if(_isNumber)
            {
                float number = float.Parse(txt);
                _dummyText.text = _preText + number.ToString(_numberFormat, CultureInfo.CreateSpecificCulture(_culture)) + _postText;
            } else
            {
                _dummyText.text = _preText + txt + _postText;
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


