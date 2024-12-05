using AgroVision.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace AgroVision
{
    public class DJIMissionBuilder
    {

        #region Propriedades
        public string Name { get; set; }

        public string User { get; set; }

        public string Model { get; set; }

        public string Camera { get; set; }

        public DrawSupport Support { get; set; }

        public List<DJIMissionOption> Options { get; set; }

        public List<NPoint> Points { get; set; }
        #endregion

        #region Inicializar
        public DJIMissionBuilder(string name, string user, string model, string camera, DrawSupport support)
        {
            Name = name;
            User = user;
            Model = model;
            Camera = camera;
            Support = support;
            Options = new List<DJIMissionOption>();
            Points = new List<NPoint>();
        }
        #endregion

        #region Métodos
        public void SaveFile(string filePath)
        {
            XmlDocument doc = new XmlDocument();

            XmlElement root = doc.CreateElement("mission");
            root.Attributes.Append(Attribute("name", Name, doc));
            root.Attributes.Append(Attribute("user", User, doc));
            root.Attributes.Append(Attribute("model", Model, doc));
            root.Attributes.Append(Attribute("camera", Camera, doc));
            root.Attributes.Append(Attribute("date", DateTime.Now.ToShortDateString(), doc));

            for (int optionIndex = 0; optionIndex < Options.Count; optionIndex++)
            {
                DJIMissionOption djiOption = Options[optionIndex];

                XmlNode option = doc.CreateElement("option");
                option.Attributes.Append(Attribute("name", djiOption.Name, doc));
                option.InnerText = djiOption.Value;

                root.AppendChild(option);
            }

            XmlNode polygon = doc.CreateElement("polygon");
            polygon.InnerText = string.Join(" ", Support.GetPolygon().Points.Select(point => $"{point.Values[0]} {point.Values[1]}"));
            root.AppendChild(polygon);

            //Notas
            for(int noteIndex = 0; noteIndex < Support.Notes.Count; noteIndex++)
            {
                XmlNode note = doc.CreateElement("note");

                NPoint point = Support.Notes[noteIndex].Point;

                note.Attributes.Append(Attribute("coord", $"{point.Values[0]} {point.Values[1]}", doc));
                note.InnerText = Support.Notes[noteIndex].Content;
                root.AppendChild(note);
            }

            XmlNode wayPoints = doc.CreateElement("waypoints");
            wayPoints.InnerText = string.Join(" ", Points.Select(wayPoint => $"{wayPoint.Values[0]} {wayPoint.Values[1]}"));
            root.AppendChild(wayPoints);

            doc.AppendChild(root);

            doc.Save(filePath);
        }

        public XmlAttribute Attribute(string name, string value, XmlDocument doc)
        {
            XmlAttribute att = doc.CreateAttribute(name);
            att.Value = value;

            return att;
        }
        #endregion

    }
}