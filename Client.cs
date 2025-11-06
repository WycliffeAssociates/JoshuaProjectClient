using JoshuaProjectClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JoshuaProjectClient
{
    public class Client
    {
        private string apiKey;
        private string baseUrl = "https://api.joshuaproject.net/v1/";
        private readonly HttpClient httpClient;
        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        
        public Client(string apiKey)
        {
            this.apiKey = apiKey;
            this.httpClient = new HttpClient();
        }

        #region Languages
        /// <summary>
        /// Get all the languages in the Joshua Project API
        /// </summary>
        /// <returns>A list of Joshua Project Languages</returns>
        public List<JPLanguage> GetAllLanguages()
        {
            List<JPLanguage> output = new List<JPLanguage>();
            int page = 0;
            bool done = false;
            while (!done)
            {
                var result = httpClient.GetAsync($"{this.baseUrl}languages.json?api_key={apiKey}&page={page}").Result;
                if (result.IsSuccessStatusCode)
                {
                    List<JPLanguage> tmp = JsonSerializer.Deserialize<List<JPLanguage>>(result.Content.ReadAsStringAsync().Result, jsonOptions);
                    if (tmp != null && tmp.Count > 0)
                    {
                        output.AddRange(tmp);
                    }
                    else
                    {
                        done = true;
                    }
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    done = true;
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException("Got unauthorized from Joshua Project check your api key");
                }
                else
                {
                    throw new Exception($"Unknown response code {result.StatusCode.ToString()}");
                }
                page++;
            }
            return output;
        }

        /// <summary>
        /// Get a single language from the Joshua Project API
        /// </summary>
        /// <param name="id">The ISO 639 of the Language</param>
        /// <returns></returns>
        public JPLanguage GetLanguage(string id)
        {
            var result = httpClient.GetAsync($"{this.baseUrl}languages/{id}.json?api_key={apiKey}").Result;
            if (result.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<JPLanguage>(result.Content.ReadAsStringAsync().Result, jsonOptions);
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException($"Language with code {id} wasn't found");
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("Got unauthorized from Joshua Project check your api key");
            }
            else
            {
                throw new Exception($"Unknown response code {result.StatusCode.ToString()}");
            }
        }
        #endregion

        /// <summary>
        /// Get all people groups in the Joshua Project
        /// </summary>
        /// <returns>A list of People Groups</returns>
        public List<JPPeopleGroup> GetAllPeopleGroups()
        {
            List<JPPeopleGroup> output = new List<JPPeopleGroup>();
            int page = 0;
            bool done = false;
            while (!done)
            {
                var result = httpClient.GetAsync($"{this.baseUrl}people_groups.json?api_key={apiKey}&page={page}").Result;
                if (result.IsSuccessStatusCode)
                {
                    List<JPPeopleGroup> tmp = JsonSerializer.Deserialize<List<JPPeopleGroup>>(result.Content.ReadAsStringAsync().Result, jsonOptions);
                    if (tmp != null && tmp.Count > 0)
                    {
                        output.AddRange(tmp);
                    }
                    else
                    {
                        done = true;
                    }
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    done = true;
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException("Got unauthorized from Joshua Project check your api key");
                }
                else
                {
                    throw new Exception($"Unknown response code {result.StatusCode.ToString()}");
                }
                page++;
            }
            return output;
        }

        /// <summary>
        /// Get a single people group from the Joshua Project API
        /// </summary>
        /// <param name="peopleGroupId">The Id of the people group to get</param>
        /// <returns>The individual people group</returns>
        public JPPeopleGroup GetPeopleGroup(string peopleGroupId)
        {
            var result = httpClient.GetAsync($"{this.baseUrl}people_groups/{peopleGroupId}.json?api_key={apiKey}").Result;
            if (result.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<JPPeopleGroup>(result.Content.ReadAsStringAsync().Result, jsonOptions);
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException($"People Group with id {peopleGroupId} wasn't found");
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("Got unauthorized from Joshua Project check your api key");
            }
            else
            {
                throw new Exception($"Unknown response code {result.StatusCode.ToString()}");
            }
        }
    }
}
