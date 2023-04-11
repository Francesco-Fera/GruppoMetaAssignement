// See https://aka.ms/new-console-template for more information
using GruppoMetaAssignement;
var xmlParser = new XmlParser(@"C:\Users\franc\Desktop\GruppoMetaAssignement\GruppoMetaAssignement\Files\myConfig.xml");
Console.WriteLine(xmlParser.Get("longtext"));
Console.WriteLine(xmlParser.Get("thumbnail/width"));
Console.WriteLine(xmlParser.Get("value3"));

