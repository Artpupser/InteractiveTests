using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class FileManager
{
    public static string DefaultPath = $"{Application.dataPath}\\";
    public static string GetPath(string name) => $"{DefaultPath}{name}";

    public static bool SaveFile<T>(string name, T data) where T : IJsonData
    {
        try
        {
            var path = GetPath(name);
            var json = data.Serialize();
            File.WriteAllText(path, json);
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
            return false;
        }
    }
    public static bool LoadFile<T>(string name, out T output) where T : IJsonData 
    {
        output = default;
        try
        {
            var path = GetPath(name);
            if (!File.Exists(path))
            {
                Debug.LogWarning($"File not finded, {path}");
                return false;
            }
            var json = File.ReadAllText(path);
            output = JsonConvert.DeserializeObject<T>(json);
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e);
            return false;
        }
    }
    public static bool SaveEncodeFile<T>(string name, T data) where T : IJsonData
    {
        try
        {
            var path = GetPath(name);
            var json = data.Serialize();
            var encode = Base64Encode(json);
            File.WriteAllText(path, encode);
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e);
            return false;
        }
    }
    public static bool LoadDecodeFile<T>(string name, out T output) where T : IJsonData
    {
        output = default;
        try
        {
            var path = GetPath(name);
            if (!File.Exists(path))
            {
                Debug.LogError($"File not finded, {path}");
                return false;
            }
            var content = File.ReadAllText(path);
            var decode = Base64Decode(content);
            output = JsonConvert.DeserializeObject<T>(decode);
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
            return false;
        }
    }
    public static bool SaveEncodeFileToPath<T>(string path, T data) where T : IJsonData
    {
        try
        {
            var json = data.Serialize();
            var encode = Base64Encode(json);
            File.WriteAllText(path, encode);
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e);
            return false;
        }
    }
    public static bool LoadDecodeFileToPath<T>(string path, out T output) where T : IJsonData
    {
        output = default;
        try
        {

            if (!File.Exists(path))
            {
                Debug.LogError($"File not finded, {path}");
                return false;
            }
            var content = File.ReadAllText(path);
            var decode = Base64Decode(content);
            output = JsonConvert.DeserializeObject<T>(decode);
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
            return false;
        }
    }
    //Шифровка
    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
    //Расшифровка
    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
}