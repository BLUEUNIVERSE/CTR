using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using System.Text;
using System.Security.Cryptography;
using System.Collections;

public static class Util
{
	/// <summary>
	/// Gets the current UTC timestamp.
	/// </summary>
	/// <value>
	/// The current UTC timestamp.
	/// </value>
	public static int CurrentUTCTimestamp {
		get {
			return (int)ToUnixTimestamp(DateTime.UtcNow);
		}
	}
	
	public static string DeviceId{
		get{
//			if(LocalStorage.Instance.HasKey("device_id")){
//				var deviceId = LocalStorage.Instance.GetString("device_id");
//				
//				if(string.IsNullOrEmpty(deviceId)){
//					deviceId = new System.Guid().ToString();
//					LocalStorage.Instance.SetString("device_id", deviceId);
//				}
//				
//				return deviceId;
//			}
//			else{
//				var deviceId = new System.Guid().ToString ();
//				LocalStorage.Instance.SetString("device_id", deviceId);
//				return deviceId;
//			}
			
			return SystemInfo.deviceUniqueIdentifier;
		}
	}
	
	public static string DeviceModel{
		get{
			return SystemInfo.deviceModel;
		}
	}
	
	/// <summary>
	/// Serializes to XML.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="obj">The obj.</param>
	/// <param name="filePath">The filepath.</param>
	public static void SerializeToXML<T> (T obj, string filePath)
	{
		Log ("Starting XML Serialization");
		using (FileStream file = new FileStream(filePath, FileMode.Create)) {
			XmlSerializer serializer = new XmlSerializer (typeof(T));
			serializer.Serialize (file, obj);
		}
		Log ("XML Serialization Complete - " + filePath);
	}

	/// <summary>
	/// DeSerialize from file.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="filePath">The file path.</param>
	/// <returns></returns>
	public static T DeSerializeFromFile<T> (string filePath)
	{
		T obj = default(T);
		if (File.Exists (filePath)) {
			//Debug.Log ("Starting XML DeSerialization - " + filePath);
			XmlSerializer serializer = new XmlSerializer (typeof(T));
			using (FileStream file = new FileStream(filePath, FileMode.Open)) {
				obj = (T)serializer.Deserialize (file);
			}
			Log ("DeSerialization Complete");
		}
		return obj;
	}
	
	/// <summary>
	/// Reads the text from resource.
	/// </summary>
	/// <returns>
	/// The text from resource.
	/// </returns>
	/// <param name='resourceName'>
	/// Resource name.
	/// </param>
	public static string ReadTextFromResource (string resourceName)
	{
		TextAsset data = (TextAsset)Resources.Load (resourceName, typeof(TextAsset));
		return null != data ? data.text : null;
	}

	/// <summary>
	/// DeSerialize from resource.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="resourceName">Name of the resource.</param>
	/// <returns></returns>
	public static T DeSerializeFromResource<T> (string resourceName)
	{
		T obj = default(T);
		string text = ReadTextFromResource (resourceName);
		if (null != text) {
			using (StringReader reader = new StringReader(text)) {
				XmlSerializer serializer = new XmlSerializer (typeof(T));
				obj = (T)serializer.Deserialize (reader);
			}
		}
		return obj;
	}

	/// <summary>
	/// Filters the list.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list">The list.</param>
	/// <param name="comparator">The comparator.</param>
	/// <returns></returns>
	public static List<T> FilterList<T> (IEnumerable<T> list, Func<T, bool> comparator)
	{
		List<T> filteredList = new List<T> ();
		foreach (T item in list) {
			if (comparator (item)) {
				filteredList.Add (item);
			}
		}
		return filteredList;
	}

	/// <summary>
	/// Flattens hierarchy
	/// </summary>
	/// <returns>
	/// The many.
	/// </returns>
	/// <param name='source'>
	/// Source.
	/// </param>
	/// <param name='selector'>
	/// Selector.
	/// </param>
	/// <typeparam name='T'>
	/// The 1st type parameter.
	/// </typeparam>
	/// <typeparam name='TResult'>
	/// The 2nd type parameter.
	/// </typeparam>
	public static List<TResult> SelectMany<T, TResult>(IEnumerable<T> source, Func<T, IEnumerable<TResult>> selector) {
		List<TResult> list = new List<TResult>();
		foreach(T item in source) {
			list.AddRange(selector(item));
		}
		return list;
	}

	/// <summary>
	/// Firsts the or default.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list">The list.</param>
	/// <param name="comparator">The comparator.</param>
	/// <returns></returns>
	public static T FirstOrDefault<T> (List<T> list, Func<T, bool> comparator)
	{
		foreach (T item in list) {
			if (comparator (item)) {
				return item;
			}
		}
		return default(T);
	}
	
	/// <summary>
	/// Uniques the list.
	/// </summary>
	/// <returns>
	/// The list.
	/// </returns>
	/// <param name='list'>
	/// List.
	/// </param>
	/// <typeparam name='T'>
	/// The 1st type parameter.
	/// </typeparam>
	public static List<T> UniqueList<T> (List<T> list)
	{
		List<T> uniqueList = new List<T> ();
		foreach (T item in list) {
			if (!uniqueList.Contains (item))
				uniqueList.Add (item);
		}
		return uniqueList;
	}
	
	/// <summary>
	/// Contains the specified data and value.
	/// </summary>
	/// <param name='data'>
	/// If set to <c>true</c> data.
	/// </param>
	/// <param name='value'>
	/// If set to <c>true</c> value.
	/// </param>
	public static bool Contains (string[] data, string value)
	{
		foreach (string s in data) {
			if (s == value)
				return true;
		}
		return false;
	}
	
	/// <summary>
	/// Adds the element to array.
	/// </summary>
	/// <param name='array'>
	/// Array.
	/// </param>
	/// <param name='newItem'>
	/// New item.
	/// </param>
	/// <typeparam name='T'>
	/// The 1st type parameter.
	/// </typeparam>
	public static void AddElementToArray<T> (ref T[] array, T newItem)
	{
		Array.Resize (ref array, array.Length + 1);
		array [array.Length - 1] = newItem;
		//Log("AddElementToArray" + newItem);
	}
	
	/// <summary>
	/// Shuffle the specified list.
	/// </summary>
	/// <param name='list'>
	/// List.
	/// </param>
	/// <typeparam name='T'>
	/// The 1st type parameter.
	/// </typeparam>
	public static List<T> Shuffle<T> (List<T> list)
	{
		System.Random rng = new System.Random ();
		List<T> shuffleList = new List<T> (list);
		int n = list.Count;
		while (n > 1) {
			n--;
			int k = rng.Next (n + 1);
			T value = shuffleList [k];
			shuffleList [k] = shuffleList [n];
			shuffleList [n] = value;  
		}
		return shuffleList;
	}
	
	/// <summary>
	/// Shuffle the specified list.
	/// </summary>
	/// <param name='list'>
	/// List.
	/// </param>
	/// <typeparam name='T'>
	/// The 1st type parameter.
	/// </typeparam>
	public static T[] Shuffle<T> (T[] list)
	{
		return Shuffle (new List<T> (list)).ToArray ();
	}
	
	/// <summary>
	/// Fors the each.
	/// </summary>
	/// <param name='list'>
	/// List.
	/// </param>
	/// <param name='action'>
	/// Action.
	/// </param>
	/// <typeparam name='T'>
	/// The 1st type parameter.
	/// </typeparam>
	public static void ForEach<T> (IEnumerable<T> list, Action<T> action)
	{
		foreach (T item in list) {
			action (item);
		}
	}
	
	/// <summary>
	/// Count the specified items in the list using the comparator.
	/// </summary>
	/// <param name='list'>
	/// List.
	/// </param>
	/// <param name='action'>
	/// Action.
	/// </param>
	/// <typeparam name='T'>
	/// The 1st type parameter.
	/// </typeparam>
	public static int Count<T> (IEnumerable<T> list, Func<T, bool> comparator)
	{
		return FilterList<T> (list, comparator).Count;
	}
	
	public static bool Contains<T> (IEnumerable<T> list, T value) {
		foreach(T item in list) {
			if(item.Equals(value)) {
				return true;
			}
		}
		return false;
	}
	
	/// <summary>
	/// Convert to unix timestamp.
	/// </summary>
	/// <returns>
	/// The unix timestamp.
	/// </returns>
	/// <param name='dt'>
	/// Date time object
	/// </param>
	public static long ToUnixTimestamp (System.DateTime dt)
	{
		DateTime unixRef = new DateTime (1970, 1, 1, 0, 0, 0);
		return (dt.Ticks - unixRef.Ticks) / 10000000;
	}
	
	/// <summary>
	/// Convert from the unix timestamp.
	/// </summary>
	/// <returns>
	/// The date time object.
	/// </returns>
	/// <param name='timestamp'>
	/// Timestamp.
	/// </param>
	public static DateTime FromUnixTimestamp (long timestamp)
	{
		DateTime unixRef = new DateTime (1970, 1, 1, 0, 0, 0);
		return unixRef.AddSeconds (timestamp);
	}
	
	/// <summary>
	/// DeSerializes the JSON string.
	/// </summary>
	/// <returns>
	/// Dictionary object.
	/// </returns>
	/// <param name='jsonStr'>
	/// Json string.
	/// </param>
	public static IDictionary<string, object> DeSerializeJSON(string jsonStr) {
		Log("[JSON] " + Environment.NewLine + jsonStr);
		return (IDictionary<string, object>)SimpleJson.SimpleJson.DeserializeObject(jsonStr);
	}

    /// <summary>
    /// SHA1 encode.
    /// </summary>
    /// <param name="plainText">The plain text.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static string SHA1Encode(string plainText) {
        var inputBytes = ASCIIEncoding.ASCII.GetBytes(plainText);        
        var hashBytes = new SHA1Managed().ComputeHash(inputBytes);
		StringBuilder hashValue = new StringBuilder();
        Array.ForEach<byte>(hashBytes, b => hashValue.Append(b.ToString("x2")));
        return hashValue.ToString();
    }

	/// <summary>
	/// Executes the post command.
	/// </summary>
	/// <returns>
	/// The post command.
	/// </returns>
	/// <param name='url'>
	/// URL.
	/// </param>
	/// <param name='data'>
	/// Data.
	/// </param>
	/// <param name='responseCode'>
	/// Response code.
	/// </param>
	public static IEnumerator ExecutePostCommand(string url, string data, Action<WWW> callback) {
		var form = new WWWForm();
		form.headers["Content-Type"] = "application/json";
		
		Log("[HTTP POST] POST Request" + Environment.NewLine + data);
		
		WWW www = new WWW(url, Encoding.UTF8.GetBytes(data), form.headers);
		yield return www;
		
		Log("[HTTP POST] POST Response  - " + url + ((!string.IsNullOrEmpty(www.error)) ? (" Error - " + www.error) : string.Empty));
		if(www.isDone && null != callback) {
			callback(www);
		}		
	}
	
	/// <summary>
	/// Executes the get command.
	/// </summary>
	/// <returns>
	/// The get command.
	/// </returns>
	/// <param name='url'>
	/// URL.
	/// </param>
	/// <param name='callback'>
	/// Callback.
	/// </param>
	public static IEnumerator ExecuteGetCommand(string url, Action<WWW> callback) {
		Log("[HTTP GET] GET Request " + Environment.NewLine + url);
		WWW www = new WWW(url);
		yield return www;
		Log("[HTTP GET] GET Response  - " + url + ((!string.IsNullOrEmpty(www.error)) ? (" Error - " + www.error) : string.Empty));
		if(www.isDone && null != callback) {
			callback(www);
		}
	}

	/// <summary>
	/// Log the specified message.
	/// </summary>
	/// <param name="message">Message.</param>
	public static void Log(object message) {
		//Debug.Log(message);
	}

	/// <summary>
	/// Log the specified message and context.
	/// </summary>
	/// <param name="message">Message.</param>
	/// <param name="context">Context.</param>
	public static void Log(object message, UnityEngine.Object context) {
		//Debug.Log(message, context);
	}

	public static string ConvertToString(Dictionary<string, string> d)
	{
		// Build up each line one-by-one and then trim the end
		StringBuilder builder = new StringBuilder();
		foreach (KeyValuePair<string, string> pair in d)
		{
			builder.Append(pair.Key).Append(":").Append(pair.Value).Append(',');
		}
		string result = builder.ToString();
		// Remove the final delimiter
		result = result.TrimEnd(',');
		return result;
	}
	
}