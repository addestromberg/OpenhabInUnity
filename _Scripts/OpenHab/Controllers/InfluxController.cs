using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Vibrant.InfluxDB.Client;
using Vibrant.InfluxDB.Client.Rows;

/// <summary>
/// Influx query controller.
/// -----------------------
/// 
/// This controller is used instead of ItemController
/// when we want to query for historical data from InfluxDB.
/// 
/// This is at the moment very simple and you may only query for
/// a fixed set for one Item. No subscription. You might wan't to use a refresh period
/// or similar in UI. Should cover most usage in an HabPanel like UI.
/// </summary>
/// 
namespace se.Studio13.OpenHabUnity
{
    public class InfluxController : MonoBehaviour
    {
        private string _InfluxHost;
        private string _Username;
        private string _Password;
        private string _Database;

        /// <summary>
        /// Initialize component from item or widget
        /// </summary>
        /// <param name="host">Must have a host</param>
        /// <param name="database">Must have a db</param>
        /// <param name="username">null or Username when auth enabled</param>
        /// <param name="password">null or Password when auth enabled</param>
        public void Initialize(string host, string database, string username = null, string password = null)
        {
            _InfluxHost = host;
            _Database = database;
            if (username != null) _Username = username;
            if (password != null) _Password = password;
        }

        /// <summary>
        /// Query the database and return the dataset async
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<InfluxResultSet<DynamicInfluxRow>> SimpleQuery(string query)
        {
            InfluxClient client = new InfluxClient(new Uri(_InfluxHost), _Username, _Password);
            //Debug.Log("Querying Database with: " + query);
            InfluxResultSet<DynamicInfluxRow> resultSet = await client.ReadAsync<DynamicInfluxRow>(_Database, query);
            client.Dispose();
            return resultSet;
        }
    }
}

