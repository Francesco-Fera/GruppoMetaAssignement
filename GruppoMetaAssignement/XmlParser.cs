using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace GruppoMetaAssignement;

public class XmlParser
{

    private readonly Dictionary<string, object> _configValues = new Dictionary<string, object>();
    private DateTime _lastModified;
    private readonly string _configFile;

    public XmlParser(string filePath)
    {
        _configFile = filePath;
        LoadConfig(_configFile);
    }

    public object? Get(string key)
    {
        return _configValues.ContainsKey(key) ? _configValues[key] : null;
    }

    private void LoadConfig(string _configFile)
    {
        var fileInfo = new FileInfo(_configFile);

        if (_lastModified != fileInfo.LastWriteTimeUtc)
        {
            _lastModified = fileInfo.LastWriteTimeUtc;

            var document = XDocument.Load(_configFile);

            foreach (var element in document.Descendants())
            {
                switch (element.Name.LocalName)
                {
                    case "Import":
                        LoadConfig($@"..\\..\\..\\Files\\{element.Attribute("src").Value}");
                        break;
                    case "Param":
                        LoadParam(element);
                        break;
                }
            }
        }
    }

    private void LoadParam(XElement element)
    {
        var name = element.Attribute("name")?.Value;
        var value = element.Attribute("value")?.Value;
        var key = GenerateKey(element, name);

        if (string.IsNullOrEmpty(name))
        {
            throw new XmlException("Missing name attribute for parameter.");
        }

        if (name.EndsWith("[]"))
        {
            HandleArrayParam(name, value);
        }
        else
        {
            HandleScalarParam(element, value, key);
        }
    }

    private void HandleScalarParam(XElement element, string? value, string key)
    {

        if (bool.TryParse(value, out var boolValue))
        {
            AddToConfigValues(key, boolValue);
        }
        else if (int.TryParse(value, out var intValue))
        {
            AddToConfigValues(key, intValue);
        }
        else
        {
            AddToConfigValues(key, value);
        }
    }

    private void HandleArrayParam(string? name, string? value)
    {
        var arrayName = name.Substring(0, name.Length - 2);
        AddToConfigValues(arrayName, value);
    }

    private void AddToConfigValues(string key, object value)
    {
        if (_configValues.ContainsKey(key))
        {
            _configValues[key] = value;
        }
        else
        {
            _configValues.Add(key, value);
        }
    }

    private string GenerateKey(XElement element, string name)
    {
        var groupNames = element.AncestorsAndSelf()
                .Where(a => a.Name.LocalName == "Group")
                .Select(a => a.Attribute("name").Value)
                .Reverse();

        return groupNames.Any() ? string.Join("/", groupNames) + "/" + name : name;
    }
}
