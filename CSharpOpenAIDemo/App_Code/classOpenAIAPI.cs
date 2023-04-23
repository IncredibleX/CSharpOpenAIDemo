using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Ajax.Utilities;
using System.IO;

/// <summary>
/// Zusammenfassungsbeschreibung für classOpenAIAPI
/// </summary>
public class TSOpenAIAPI
{
    private string Key;

    public TSOpenAIAPI(OpenAIApiKey oOpenAIApiKey)
    {
        this.Key = oOpenAIApiKey.Key;
    }


    public class OpenAIApiKey
    {
        public string Key { get; set; }
    }

    public string callOpenAI(int tokens, string input, string engine, double temperature, int topP, int frequencyPenalty, int presencePenalty)
    {

        var apiCall = "https://api.openai.com/v1/engines/" + engine + "/completions";
        //var apiCall = "https://api.openai.com/v1/chat/completions";
        try
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), apiCall))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + this.Key);
                    request.Content = new StringContent("{\n  \"prompt\": \"" + input + "\",\n  \"temperature\": " +
                                                        temperature.ToString(CultureInfo.InvariantCulture) + ",\n  \"max_tokens\": " + tokens + ",\n  \"top_p\": " + topP +
                                                        ",\n  \"frequency_penalty\": " + frequencyPenalty + ",\n  \"presence_penalty\": " + presencePenalty + "\n}");

                    //request.Content = new StringContent("{\n  \"model\": \"gpt-3.5-turbo\",\n  \"messages\": [{\"role\": \"user\", \"content\": \"liste der suchbegriffe für ###Weinflaschenhalter Metall Figuren Kapitän zur See### in einer zeile mit leerzeichen getrennt\"}]}");

                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    var response = httpClient.SendAsync(request).Result;
                    var json = response.Content.ReadAsStringAsync().Result;
                    dynamic dynObj = JsonConvert.DeserializeObject(json);
                    if (dynObj != null)
                    {
                        return dynObj.choices[0].text.ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return null;
    }

    public TSOpenAIAPI.Images callOpenAICreateImage(string Requesttext)
    {
        var apiCall = "https://api.openai.com/v1/images/generations";
        TSOpenAIAPI.Images oImages = new Images();
        
        try
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), apiCall))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + this.Key);

                    string sJsonContent = "{\n  \"prompt\": \"" + Requesttext + "\",\n  \"n\": 2, \"size\" : \"1024x1024\"}";
                    request.Content = new StringContent(sJsonContent);

                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    var response = httpClient.SendAsync(request).Result;
                    var json = response.Content.ReadAsStringAsync().Result;
                        
                    oImages.Response = oImages.Response = response.ReasonPhrase;
                    if (response.ReasonPhrase == "OK")
                        oImages = (TSOpenAIAPI.Images)JsonConvert.DeserializeObject<TSOpenAIAPI.Images>(json.ToString());
                    
                        

                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return oImages;
    }

    public class Images
    {
        public Int64 created { get; set; }
        public List<_Data> data { get; set; }
        public string Response { get; set; }
    }

    public class _Data
    {
        public string url { get; set; }
    }

    
}
