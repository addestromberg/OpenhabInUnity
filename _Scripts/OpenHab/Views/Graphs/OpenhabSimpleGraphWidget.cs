using System.Collections;
using UnityEngine;
using Vibrant.InfluxDB.Client;
using Vibrant.InfluxDB.Client.Rows;
//using ChartAndGraph;

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
    public class OpenhabSimpleGraphWidget : MonoBehaviour
    {
        [Header("Item & Influx Setup")]
        [Tooltip("Server url with port if needed. ie. http://localhost:8086 for InfluxDB")]
        public string _Server = "http://openhab:8086";
        [Tooltip("Database name in InfluxDB")]
        public string _Database = "openhab_db";
        [Tooltip("Retention policy in InfluxDB")]
        public string _RetentionPolicy = "autogen";
        [Tooltip("Username with read permissions on DB in InfluxDB")]
        public string _Username = "grafana";
        [Tooltip("User password for Database")]
        public string _Password ;
        [Tooltip("Item name in openhab. ie. gf_Hallway_Light")]
        public string _Item;
        [Tooltip("Refreshrate in seconds")]
        public float _Refreshrate = 60f;

        [Header("Widget Setup")]
        [Tooltip("InfluxDB readable time syntax. ie. now() - 24h")]
        public string _TimeSpan = "now() - 24h"; 

        private InfluxController _influxController;
        //private GraphChart _graph; // Chart And Graph asset in unity store.

        /// <summary>
        /// Initialize ItemController
        /// </summary>
        void Start()
        {
            // Add or get controller component
            if (GetComponent<InfluxController>() != null)
            {
                _influxController = GetComponent<InfluxController>();
            }
            else
            {
                _influxController = gameObject.AddComponent<InfluxController>();
            }

            if (_Username != "" && _Password != "")
            {
                _influxController.Initialize(_Server, _Item, _Username, _Password);
            } else
            {
                _influxController.Initialize(_Server, _Item);
            }


            
            InitWidget();

        }

        /// <summary>
        /// For public field initialization etc. This is to be able to use
        /// a generic start function for all widgets. This function is called for
        /// at end of Start()
        /// </summary>
        private void InitWidget()
        {
            // if (_graph == null) _graph = GetComponent<GraphChart>();
            
            StartCoroutine(RefreshGraph()); // Lazyguy timer
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

        }

        /// <summary>
        /// Perform your graph presentation stuff here. (Updates every refresh interval)
        /// </summary>
        private async void DrawData()
        {
            InfluxResultSet<DynamicInfluxRow> result = await _influxController.SimpleQuery(QueryBuilder.ItemTimeSpan(_Database, _RetentionPolicy, _Item, _TimeSpan));
            //var series = result.Results[0].Series[0].Name;

            var series = result.Results[0].Series[0];
            Debug.Log("result: " + result.Results[0].Series.Count + " rows: " + series.Rows.Count);

            /// This is how you can do if using Chart And Graph.
            /**
            if (_graph != null)
            {
                _graph.DataSource.StartBatch();  // start a new update batch
                _graph.DataSource.ClearCategory("CPU Load");  // clear the categories we have created in the inspector

                foreach (DynamicInfluxRow row in series.Rows)
                {
                    _graph.DataSource.AddPointToCategory("CPU Load", row.GetTimestamp().Value, float.Parse(row.GetField("value").ToString()));                    
                }
                _graph.DataSource.EndBatch(); // end the update batch . this call will render the graph
            }
            */
            
        }

        /// <summary>
        /// Simple timer for updates.
        /// </summary>
        /// <returns></returns>
        IEnumerator RefreshGraph()
        {
            for (;;)
            {
                DrawData();
                yield return new WaitForSeconds(_Refreshrate);
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

        }
    }
}