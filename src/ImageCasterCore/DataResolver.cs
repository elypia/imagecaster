using System;
using System.Collections.Generic;
using System.Linq;
using ImageCasterCore.Api;
using ImageCasterCore.Collectors;
using ImageCasterCore.Configuration;
using ImageCasterCore.Extensions;
using NLog;

namespace ImageCasterCore
{
    /// <summary>
    /// Resolve data from data sources, this could be the primary input data
    /// or additional data that's required to execute certain actions.
    /// </summary>
    public class DataResolver
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        /// <summary>
        /// A list of all primary inputs.
        /// This is used for any primary action required.
        /// </summary>
        public List<ResolvedData> Data { get; }

        /// <summary>
        /// A list of any additional inputs required.
        /// This is used for anything that might be required against the primary input
        /// such as clipping masks.
        /// </summary>
        public Dictionary<string, List<ResolvedData>> AdditionalData { get; }

        public DataResolver(DataSource source) : this(new List<DataSource>(){source})
        {
            
        }
        
        public DataResolver(List<DataSource> sources)
        {
            Data = Resolve(sources);
            AdditionalData = new Dictionary<string, List<ResolvedData>>();
        }

        /// <summary>Overload of List method.</summary>
        /// <param name="name">An alias to refer to this set of data by.</param>
        /// <param name="source">The datasource to resolve.</param>
        /// <returns></returns>
        public List<ResolvedData> ResolveAdditional(string name, DataSource source)
        {
            return ResolveAdditional(name, new List<DataSource>(){source});
        }
        
        /// <summary>
        /// Resolve additional data for processing.
        /// This adds the resolved items to a dictionary for reference by
        /// the name specieid.
        /// </summary>
        /// <param name="name">An alias to refer to this set of data by.</param>
        /// <param name="sources">The datasources to resolve.</param>
        /// <returns>A list of resolved data.</returns>
        public List<ResolvedData> ResolveAdditional(string name, List<DataSource> sources)
        {
            List<ResolvedData> data = Resolve(sources);
            AdditionalData.Add(name, data);
            return data;
        }

        public ResolvedData ResolvedData(string key, ResolvedData input, string pattern)
        {
            List<ResolvedData> allData = AdditionalData[key];
            string name = CompilePattern(input, pattern);
            ResolvedData result = allData.Find((data) => data.Name == name);
            return result;
        }

        public string CompilePattern(ResolvedData input, string pattern)
        {
            input.RequireNonNull();

            if (pattern == null)
            {
                return input.Name;
            }
            
            string[] tokens = input.Tokens;
            
            for (int i = tokens.Length - 1; i >= 0; i--)
            {
                string token = tokens[i];
                pattern = pattern.Replace("$" + (i), token);
            }

            Logger.Trace("Resolved file: {0}", pattern);
            return pattern;
        }
        
        /// <summary>
        /// Resolve data from a list of datasources, these
        /// could be from various locations or the same.
        /// </summary>
        /// <param name="sources">All data sources to load.</param>
        /// <returns>A list of all resolved data from the data sources.</returns>
        public List<ResolvedData> Resolve(List<DataSource> sources)
        {
            IEnumerable<ResolvedData> data = new List<ResolvedData>();
                
            foreach (DataSource source in sources)
            {
                ICollector collector = ResolveCollector(source.Collector);
                List<ResolvedData> resolvedData = collector.Collect(source.Source);
                data = data.Concat(resolvedData);
            }

            return data.ToList();
        }

        /// <param name="collector">The identifier for which collector to use.</param>
        /// <returns>The collector implementation to collect the input.</returns>
        /// <exception cref="ArgumentException">
        /// If an invalid datasource is specified. For example:
        /// <code>invalid:{data}</code>
        /// </exception>
        public ICollector ResolveCollector(string collector)
        {
            switch (collector)
            {
                case null: case "file":
                    return new FileCollector();
                case "base64":
                    return new Base64Collector();
                case "regex":
                    return new RegexCollector();
                default:
                    throw new ArgumentException("Unknown collector specified.");
            }
        }
    }
}