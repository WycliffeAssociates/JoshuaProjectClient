﻿using JoshuaProjectClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JoshuaProjectClient
{
    public class Client
    {
        private string apiKey;
        private string baseUrl = "https://api.joshuaproject.net/v1/";
        public Client(string apiKey)
        {
            this.apiKey = apiKey;
        }

        #region Languages
        /// <summary>
        /// Get all the languages in the Joshua Project API
        /// </summary>
        /// <returns>A list of Joshua Project Languages</returns>
        public List<JPLanguage> GetAllLanguages()
        {
            List<JPLanguage> output = new List<JPLanguage>();
            using (HttpClient client = new HttpClient())
            {
                int page = 0;
                bool done = false;
                while (!done)
                {
                    var result = client.GetAsync($"{this.baseUrl}languages.json?api_key={apiKey}&page={page}").Result;
                    if (result.IsSuccessStatusCode)
                    {
                        List<JPLanguage> tmp = JsonConvert.DeserializeObject<List<JPLanguage>>(result.Content.ReadAsStringAsync().Result);
                        if (tmp.Count > 0)
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
            using (HttpClient client = new HttpClient())
            {
                var result = client.GetAsync($"{this.baseUrl}languages/{id}.json?api_key={apiKey}").Result;
                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<JPLanguage>(result.Content.ReadAsStringAsync().Result);
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
        }
        #endregion

        /// <summary>
        /// Get all people groups in the Joshua Project
        /// </summary>
        /// <returns>A list of People Groups</returns>
        public List<JPPeopleGroup> GetAllPeopleGroups()
        {
            List<JPPeopleGroup> output = new List<JPPeopleGroup>();
            using (HttpClient client = new HttpClient())
            {
                int page = 0;
                bool done = false;
                while (!done)
                {
                    var result = client.GetAsync($"{this.baseUrl}people_groups.json?api_key={apiKey}&page={page}").Result;
                    if (result.IsSuccessStatusCode)
                    {
                        List<JPPeopleGroup> tmp = JsonConvert.DeserializeObject<List<JPPeopleGroup>>(result.Content.ReadAsStringAsync().Result);
                        if (tmp.Count > 0)
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
            using (HttpClient client = new HttpClient())
            {
                var result = client.GetAsync($"{this.baseUrl}people_groups/{peopleGroupId}.json?api_key={apiKey}").Result;
                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<JPPeopleGroup>(result.Content.ReadAsStringAsync().Result);
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
}
