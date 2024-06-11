using UnityEngine;
using UnityEngine.Networking;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections;

public class Population : MonoBehaviour
{
    public string Latitude_ = "-112.07366198397744";
    public string Longitude_ = "33.46592518188455";

    [SerializeField]
    private RadiPlacement RadiPlace;
    
    [SerializeField]
    private DemographicsInfo DemographicsInfoHandler;

    //apiUrls for 1,3, and 5 miles radius within the given point respectively
    private string apiUrl1;
    private string apiUrl3;
    private string apiUrl5;

    private Attribute attributes;
    private bool dataCollected = false;

    void Update()
    {
        // Constructing the API URL properly
        //apiUrl1 = "http://geoenrich.arcgis.com/arcgis/rest/services/World/geoenrichmentserver/GeoEnrichment/enrich?StudyAreas=[{{'geometry':{{'x': "+Latitude_+"}, 'y': "+Longitude_+"}}}}]&f=pjson&token=AAPK33cc4c3d402848e5bba049a09ab1b050WZSXS4kNdNRqjiUb7VPTwHeJd7sXz9rjNCoz-TRUBha5o2fNUCPkrEwlDfBlO2fn";
        
        // Make the API call
        //GeoEnrichment();
    }
    //    private string apiUrl1 = "geoenrich.arcgis.com/arcgis/rest/services/World/geoenrichmentserver/GeoEnrichment/enrich?StudyAreas=[{'geometry':{'x': -112.07366198397744, 'y': 33.46592518188455}}]&f=pjson&token=AAPK33cc4c3d402848e5bba049a09ab1b050WZSXS4kNdNRqjiUb7VPTwHeJd7sXz9rjNCoz-TRUBha5o2fNUCPkrEwlDfBlO2fn";

    public void GeoEnrichment(GameObject map_)
    {
        StartCoroutine(MakeApiCall());
        StartCoroutine(MakeApiCall3());
        StartCoroutine(MakeApiCall5());
        RadiPlace.PlaceRadi(map_.gameObject);
    }

    IEnumerator MakeApiCall()
    {
        apiUrl1 = "https://geoenrich.arcgis.com/arcgis/rest/services/World/geoenrichmentserver/GeoEnrichment/enrich?StudyAreas=[{%27geometry%27:{%27x%27:%20" + Latitude_ + ",%20%27y%27:%20" + Longitude_ + "}}]&f=pjson&token=AAPK33cc4c3d402848e5bba049a09ab1b050WZSXS4kNdNRqjiUb7VPTwHeJd7sXz9rjNCoz-TRUBha5o2fNUCPkrEwlDfBlO2fn";
        apiUrl3 = "https://geoenrich.arcgis.com/arcgis/rest/services/World/geoenrichmentserver/GeoEnrichment/enrich?StudyAreas=[{%27geometry%27:{%27x%27:%20" + Latitude_ + ",%20%27y%27:%20" + Longitude_ + "}}]&StudyAreasOptions={%27bufferRadii%27:[%273%27]}&f=pjson&token=AAPK33cc4c3d402848e5bba049a09ab1b050WZSXS4kNdNRqjiUb7VPTwHeJd7sXz9rjNCoz-TRUBha5o2fNUCPkrEwlDfBlO2fn";
        apiUrl5 = "https://geoenrich.arcgis.com/arcgis/rest/services/World/geoenrichmentserver/GeoEnrichment/enrich?StudyAreas=[{%27geometry%27:{%27x%27:%20" + Latitude_ + ",%20%27y%27:%20" + Longitude_ + "}}]&StudyAreasOptions={%27bufferRadii%27:[%275%27]}&f=pjson&token=AAPK33cc4c3d402848e5bba049a09ab1b050WZSXS4kNdNRqjiUb7VPTwHeJd7sXz9rjNCoz-TRUBha5o2fNUCPkrEwlDfBlO2fn";


        using (UnityWebRequest www = UnityWebRequest.Get(apiUrl1))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {

                string jsonResponse = www.downloadHandler.text;
                Debug.Log("Response: " + jsonResponse);

                // Parse JSON using Newtonsoft.Json
                ResponseData responseData = JsonUtility.FromJson<ResponseData>(jsonResponse);

                // Check if the response is successful
                if (responseData.results != null && responseData.results.Count > 0)
                {
                    Result result = responseData.results[0];
                    FeatureSet featureSet = result.value.FeatureSet[0];
                    Feature feature = featureSet.features[0];
                    attributes = feature.attributes;

                    dataCollected = true;

                    DemographicsInfoHandler.PopulationUpdate(1);

                    Debug.Log("Total Population: " + attributes.TOTPOP);
                    Debug.Log("Total Households: " + attributes.TOTHH);
                    Debug.Log("Average Household Size: " + attributes.AVGHHSZ);
                    Debug.Log("Male Population: " + attributes.TOTMALES);
                    Debug.Log("Female Population: " + attributes.TOTFEMALES);
                }
                else
                {
                    Debug.LogError("No results found in the response.");
                }
            }
        }
    }

    IEnumerator MakeApiCall3()
    {
        apiUrl1 = "https://geoenrich.arcgis.com/arcgis/rest/services/World/geoenrichmentserver/GeoEnrichment/enrich?StudyAreas=[{%27geometry%27:{%27x%27:%20" + Latitude_ + ",%20%27y%27:%20" + Longitude_ + "}}]&f=pjson&token=AAPK33cc4c3d402848e5bba049a09ab1b050WZSXS4kNdNRqjiUb7VPTwHeJd7sXz9rjNCoz-TRUBha5o2fNUCPkrEwlDfBlO2fn";
        apiUrl3 = "https://geoenrich.arcgis.com/arcgis/rest/services/World/geoenrichmentserver/GeoEnrichment/enrich?StudyAreas=[{%27geometry%27:{%27x%27:%20" + Latitude_ + ",%20%27y%27:%20" + Longitude_ + "}}]&StudyAreasOptions={%27bufferRadii%27:[%273%27]}&f=pjson&token=AAPK33cc4c3d402848e5bba049a09ab1b050WZSXS4kNdNRqjiUb7VPTwHeJd7sXz9rjNCoz-TRUBha5o2fNUCPkrEwlDfBlO2fn";
        apiUrl5 = "https://geoenrich.arcgis.com/arcgis/rest/services/World/geoenrichmentserver/GeoEnrichment/enrich?StudyAreas=[{%27geometry%27:{%27x%27:%20" + Latitude_ + ",%20%27y%27:%20" + Longitude_ + "}}]&StudyAreasOptions={%27bufferRadii%27:[%275%27]}&f=pjson&token=AAPK33cc4c3d402848e5bba049a09ab1b050WZSXS4kNdNRqjiUb7VPTwHeJd7sXz9rjNCoz-TRUBha5o2fNUCPkrEwlDfBlO2fn";


        using (UnityWebRequest www = UnityWebRequest.Get(apiUrl3))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {

                string jsonResponse = www.downloadHandler.text;
                Debug.Log("Response: " + jsonResponse);

                // Parse JSON using Newtonsoft.Json
                ResponseData responseData = JsonUtility.FromJson<ResponseData>(jsonResponse);

                // Check if the response is successful
                if (responseData.results != null && responseData.results.Count > 0)
                {
                    Result result = responseData.results[0];
                    FeatureSet featureSet = result.value.FeatureSet[0];
                    Feature feature = featureSet.features[0];
                    attributes = feature.attributes;

                    dataCollected = true;

                    DemographicsInfoHandler.PopulationUpdate(3);

                    Debug.Log("Total Population: " + attributes.TOTPOP);
                    Debug.Log("Total Households: " + attributes.TOTHH);
                    Debug.Log("Average Household Size: " + attributes.AVGHHSZ);
                    Debug.Log("Male Population: " + attributes.TOTMALES);
                    Debug.Log("Female Population: " + attributes.TOTFEMALES);
                }
                else
                {
                    Debug.LogError("No results found in the response.");
                }
            }
        }
    }

    IEnumerator MakeApiCall5()
    {
        apiUrl1 = "https://geoenrich.arcgis.com/arcgis/rest/services/World/geoenrichmentserver/GeoEnrichment/enrich?StudyAreas=[{%27geometry%27:{%27x%27:%20" + Latitude_ + ",%20%27y%27:%20" + Longitude_ + "}}]&f=pjson&token=AAPK33cc4c3d402848e5bba049a09ab1b050WZSXS4kNdNRqjiUb7VPTwHeJd7sXz9rjNCoz-TRUBha5o2fNUCPkrEwlDfBlO2fn";
        apiUrl3 = "https://geoenrich.arcgis.com/arcgis/rest/services/World/geoenrichmentserver/GeoEnrichment/enrich?StudyAreas=[{%27geometry%27:{%27x%27:%20" + Latitude_ + ",%20%27y%27:%20" + Longitude_ + "}}]&StudyAreasOptions={%27bufferRadii%27:[%273%27]}&f=pjson&token=AAPK33cc4c3d402848e5bba049a09ab1b050WZSXS4kNdNRqjiUb7VPTwHeJd7sXz9rjNCoz-TRUBha5o2fNUCPkrEwlDfBlO2fn";
        apiUrl5 = "https://geoenrich.arcgis.com/arcgis/rest/services/World/geoenrichmentserver/GeoEnrichment/enrich?StudyAreas=[{%27geometry%27:{%27x%27:%20" + Latitude_ + ",%20%27y%27:%20" + Longitude_ + "}}]&StudyAreasOptions={%27bufferRadii%27:[%275%27]}&f=pjson&token=AAPK33cc4c3d402848e5bba049a09ab1b050WZSXS4kNdNRqjiUb7VPTwHeJd7sXz9rjNCoz-TRUBha5o2fNUCPkrEwlDfBlO2fn";


        using (UnityWebRequest www = UnityWebRequest.Get(apiUrl5))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {

                string jsonResponse = www.downloadHandler.text;
                Debug.Log("Response: " + jsonResponse);

                // Parse JSON using Newtonsoft.Json
                ResponseData responseData = JsonUtility.FromJson<ResponseData>(jsonResponse);

                // Check if the response is successful
                if (responseData.results != null && responseData.results.Count > 0)
                {
                    Result result = responseData.results[0];
                    FeatureSet featureSet = result.value.FeatureSet[0];
                    Feature feature = featureSet.features[0];
                    attributes = feature.attributes;

                    dataCollected = true;

                    DemographicsInfoHandler.PopulationUpdate(5);

                    Debug.Log("Total Population: " + attributes.TOTPOP);
                    Debug.Log("Total Households: " + attributes.TOTHH);
                    Debug.Log("Average Household Size: " + attributes.AVGHHSZ);
                    Debug.Log("Male Population: " + attributes.TOTMALES);
                    Debug.Log("Female Population: " + attributes.TOTFEMALES);
                }
                else
                {
                    Debug.LogError("No results found in the response.");
                }
            }
        }
    }

    public double[] GetPopulationData()
    {
        double[] result = new double[5];

        if(dataCollected)
        {
            result[0] = attributes.TOTMALES;
            result[1] = attributes.TOTFEMALES;
            result[2] = attributes.TOTPOP;
            result[3] = attributes.TOTHH;
            result[4] = attributes.AVGHHSZ;
        }

        return result;
    }
    public double[] GetHouseholdData()
    {
        double[] result = new double[3];

        if (dataCollected)
        {
            result[0] = attributes.TOTPOP;
            result[1] = attributes.TOTHH;
            result[2] = attributes.AVGHHSZ;
        }

        return result;
    }

    [Serializable]
    public class ResponseData
    {
        public List<Result> results;
    }

    [Serializable]
    public class Result
    {
        public string paramName;
        public string dataType;
        public Value value;
    }

    [Serializable]
    public class Value
    {
        public string version;
        public FeatureSet[] FeatureSet;
    }

    [Serializable]
    public class FeatureSet
    {
        public Feature[] features;
    }

    [Serializable]
    public class Feature
    {
        public Attribute attributes;
    }

    [Serializable]
    public class Attribute
    {
        public string ID;
        public int OBJECTID;
        public string sourceCountry;
        public string areaType;
        public string bufferUnits;
        public string bufferUnitsAlias;
        public double bufferRadii;
        public string aggregationMethod;
        public double populationToPolygonSizeRating;
        public double apportionmentConfidence;
        public int HasData;
        public double TOTPOP;
        public double TOTHH;
        public double AVGHHSZ;
        public double TOTMALES;
        public double TOTFEMALES;
    }
}
