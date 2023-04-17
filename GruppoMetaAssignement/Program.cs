// See https://aka.ms/new-console-template for more information
using GruppoMetaAssignement;
var xmlParser = new XmlParser(@"C:\Users\franc\Source\Repos\Francesco-Fera\GruppoMetaAssignement\GruppoMetaAssignement\Files\myConfig.xml");
var imageResizer = new ImageResizer(xmlParser);
imageResizer.Resize("thumbnail", @"C:\Users\franc\Downloads\pexels-rafael-guajardo-604671.jpg");
//Console.WriteLine(result.GetType());
//Console.WriteLine(result);

