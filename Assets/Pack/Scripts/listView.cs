using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using System;

public class listView : MonoBehaviour
{
    private string prefabName = "listprefab";

    // Start is called before the first frame update
    void Start()
    {
        if (SceneController.keyword != null)
        {
            StartCoroutine(makeURLRequest());
        }
    }

    IEnumerator makeURLRequest()
    {
        string url = "https://bhojrajbhatt.com/";
       
        string keyword = SceneController.keyword;
        

        switch (keyword)
        {
            case "gas":
                url += "gasJson.json";
                prefabName = "listPrefabFood";
                break;
            case "food":
                url += "foodJson.json";
                prefabName = "listPrefabHotel";
                break;
            case "union":
                url += "unionJson.json";
                prefabName = "listPrefabPark";
                break;
            case "parking":
                url += "parkingJson.json";
                prefabName = "listPrefabFood";
                break;
            case "rest":
                url += "restJson.json";
                prefabName = "listPrefabHotel";
                break;
            default:
                url += "restJson.json"; // Default JSON if keyword doesn't match
                prefabName = "listPrefab";
                break;
        }

        using (WWW www = new WWW(url))
        {
            yield return www;
            if (www.error == null)
                createList(www.text);
            else
                Debug.LogError("Error Loading Data: " + www.error);
        }
    }

    public void createList(string jsonString)
    {
        if (jsonString != null)
        {
            RootObject thePlaces = JsonConvert.DeserializeObject<RootObject>(jsonString);

            if (thePlaces.results != null && thePlaces.results.Count > 0)
            {
                GameObject contentHolder = GameObject.FindGameObjectWithTag("Content");

                foreach (Result result in thePlaces.results)
                {
                    GameObject thePrefab = Instantiate(Resources.Load<GameObject>("ButtonPrefabs/" + prefabName));
                    thePrefab.transform.SetParent(contentHolder.transform, false);
                    Text[] theText = thePrefab.GetComponentsInChildren<Text>();

                    theText[0].text = result.name;
                    double distance = Math.Round(GeoCodeCalc.CalcDistance(GPS.latitude, GPS.longitude, result.geometry.location.lat, result.geometry.location.lng, GeoCodeCalcMeasurement.Kilometers), 2);
                    theText[1].text = "Distance: " + distance.ToString() + "km";

                    Button button = thePrefab.GetComponentInChildren<Button>();
                    button.name = result.name; // Using name as identifier here


                    string url = $"https://www.google.com/maps/dir/{GPS.latitude},{GPS.longitude}/{result.geometry.location.lat},{result.geometry.location.lng}/@{GPS.latitude},{GPS.longitude},17z/data=!4m2!4m1!3e0";

                    //string url = $"https://www.google.com/maps/dir/33.2121743,-97.1570308/{result.geometry.location.lat},{result.geometry.location.lng}/@33.2121743,-97.1570308,17z/data=!4m2!4m1!3e0";
                    AddListener(button, url);
                }
            }
            else
            {
                Debug.Log("No results found");
            }
        }
    }

    void AddListener(Button b, string url)
    {
        b.onClick.AddListener(() => Application.OpenURL(url));
    }
}


// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Geometry
{
    public Location location { get; set; }
    public Viewport viewport { get; set; }
}

public class Location
{
    public double lat { get; set; }
    public double lng { get; set; }
}

public class Northeast
{
    public double lat { get; set; }
    public double lng { get; set; }
}

public class OpeningHours
{
    public bool open_now { get; set; }
}

public class Photo
{
    public int height { get; set; }
    public List<string> html_attributions { get; set; }
    public string photo_reference { get; set; }
    public int width { get; set; }
}

public class PlusCode
{
    public string compound_code { get; set; }
    public string global_code { get; set; }
}

public class Result
{
    public Geometry geometry { get; set; }
    public string icon { get; set; }
    public string id { get; set; }
    public string name { get; set; }
    public OpeningHours opening_hours { get; set; }
    public List<Photo> photos { get; set; }
    public string place_id { get; set; }
    public PlusCode plus_code { get; set; }
    public double rating { get; set; }
    public string reference { get; set; }
    public string scope { get; set; }
    public List<string> types { get; set; }
    public string vicinity { get; set; }
}

public class RootObject
{
    public List<object> html_attributions { get; set; }
    public List<Result> results { get; set; }
    public string status { get; set; }
}

public class Southwest
{
    public double lat { get; set; }
    public double lng { get; set; }
}

public class Viewport
{
    public Northeast northeast { get; set; }
    public Southwest southwest { get; set; }
}