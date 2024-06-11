using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Unity.VisualScripting;
using Input = UnityEngine.Input;


public class DataFetch : MonoBehaviour
{
    public string Latitude_ = "-112.07366198397744";
    public string Longitude_ = "33.46592518188455";
    public string KeyFact = "";
    public List<string> DataSets;
    private string apiUrl;
    public List<string> DataSetsDict;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J)){
            StartCoroutine(MakeApiCall(KeyFact,3));

        }
    }
    private void Start()
    {
        if (DataSetsDict == null)
        {
            DataSetsDict = new List<string>();
        }
        StartCoroutine(MakeApiCall(KeyFact));
    }
    public void GeoEnrichment()
    {
        SetCoordinate(Latitude_, Longitude_);
        StartCoroutine(MakeApiCall(KeyFact));
    }

    IEnumerator MakeApiCall(string dataKey)
    {
        // Assuming categorize is a function that you need to call
        categorize(dataKey);

        string apiUrl = $"https://geoenrich.arcgis.com/arcgis/rest/services/World/GeoEnrichmentServer/Geoenrichment/Enrich?studyareas=[{{\"geometry\":{{\"x\":{Latitude_},\"y\":{Longitude_}}}}}]&f=pjson&analysisVariables=[\"{dataKey}\"]&token=AAPK33cc4c3d402848e5bba049a09ab1b050WZSXS4kNdNRqjiUb7VPTwHeJd7sXz9rjNCoz-TRUBha5o2fNUCPkrEwlDfBlO2fn";

        using (UnityWebRequest www = UnityWebRequest.Get(apiUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to fetch data: " + www.error);
                yield break;
            }

            // Parse JSON response using Newtonsoft.Json
            string json = www.downloadHandler.text;
            Debug.Log(json);
            ProcessData(json);
        }
    }
    IEnumerator MakeApiCall(string dataKey, int k)
    {
        // Assuming categorize is a function that you need to call
        categorize(dataKey);

        string apiUrl = $"https://geoenrich.arcgis.com/arcgis/rest/services/World/GeoEnrichmentServer/Geoenrichment/Enrich?studyareas=[{{\"geometry\":{{\"x\":{Latitude_},\"y\":{Longitude_}}}}}]&StudyAreasOptions={{\"bufferRadii\": [\"{k}\"]}}&f=pjson&analysisVariables=[\"{dataKey}\"]&token=AAPK33cc4c3d402848e5bba049a09ab1b050WZSXS4kNdNRqjiUb7VPTwHeJd7sXz9rjNCoz-TRUBha5o2fNUCPkrEwlDfBlO2fn";

        using (UnityWebRequest www = UnityWebRequest.Get(apiUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to fetch data: " + www.error);
                yield break;
            }

            // Parse JSON response using Newtonsoft.Json
            string json = www.downloadHandler.text;
            Debug.Log(json);
            ProcessData(json);
        }
    }
    //&StudyAreasOptions={%27bufferRadii%27:[%273%27]}&f=pjson&token=AAPK33cc4c3d402848e5bba049a09ab1b050WZSXS4kNdNRqjiUb7VPTwHeJd7sXz9rjNCoz-TRUBha5o2fNUCPkrEwlDfBlO2fn";

    private void ProcessData(string json)
    {
        string[] Data_attrs = new string[DataSets.Count];
        DataSetsDict.Clear();
        // Parse JSON response using Newtonsoft.Json
        JObject data = JObject.Parse(json);
        if (data["results"] != null && data["results"].HasValues)
        {
            JArray features = (JArray)data["results"][0]["value"]["FeatureSet"][0]["features"];
            JArray feature_names = (JArray)data["results"][0]["value"]["FeatureSet"];
            if (features != null && features.Count > 0)
            {
                JObject attributes = (JObject)features[0]["attributes"];
                int itr = 0;
                foreach (string dataType in DataSets)
                {
                    Data_attrs[itr] = (string)attributes[dataType];
                    Debug.Log(feature_names[0]["fieldAliases"][dataType] + " : " + Data_attrs[itr]);
                    string t1 = feature_names[0]["fieldAliases"][dataType].ToString();
                    string t2 = Data_attrs[itr].ToString();
                    DataSetsDict.Add(t1);
                    DataSetsDict.Add(t2);
                    itr++;
                }
            }
        }
    }

    private void categorize(string Attr)
    {
        int cnt = 0;
        string[] splitEntries = Attr.Split(new char[] { ',', '.' });
        foreach(var k in splitEntries)
        {
            if (cnt % 2 == 1)
            {
                DataSets.Add(k);
            }
            cnt++;
        }
    }

    public void SetCoordinate(string lat, string lon)
    {
        Latitude_ = lat;
        Longitude_ = lon;
    }
    public List<string> GetData()
    {
        return DataSetsDict;
    }
    public GameObject GetObj()
    {
        GameObject obj = this.gameObject;
        return obj;
    } 
}
