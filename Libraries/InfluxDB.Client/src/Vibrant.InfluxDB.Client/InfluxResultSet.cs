﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vibrant.InfluxDB.Client
{
   /// <summary>
   /// Result of multiple queries against influxDB that does not return a table.
   /// </summary>
   /// <typeparam name="TInfluxRow"></typeparam>
   public class InfluxResultSet<TInfluxRow>
   {
      /// <summary>
      /// Constructs an InfluxResultSet with the specified results.
      /// </summary>
      /// <param name="results"></param>
      public InfluxResultSet( List<InfluxResult<TInfluxRow>> results )
      {
         Results = results;
      }

      /// <summary>
      /// Gets the results.
      /// </summary>
      public List<InfluxResult<TInfluxRow>> Results { get; set; }
   }

   /// <summary>
   /// Result of multiple queries against influxDB that returns a table.
   /// </summary>
   public class InfluxResultSet
   {
      /// <summary>
      /// Constructs an InfluxResultSet wit hthe specified results.
      /// </summary>
      /// <param name="results"></param>
      public InfluxResultSet( List<InfluxResult> results )
      {
         Results = results;
      }

      /// <summary>
      /// Gets the results.
      /// </summary>
      public List<InfluxResult> Results { get; set; }
   }
}
