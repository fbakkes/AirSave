using AirSave.GMap;
using AirSave.ViewModels;
using System;
using System.IO;
using System.Xml.Serialization;

namespace AirSave.Models
{
    [Serializable]
    public class Settings
    {
        public Settings()
        {
        }

        public void SaveSettings()
        {
            var xmlSerializer = new XmlSerializer(typeof(Settings),
                                                  new XmlRootAttribute { ElementName = "Settings" });

            using (var fileStream = File.Open("temp", FileMode.Create))
            {
                xmlSerializer.Serialize(fileStream, this);
                fileStream.Flush();
                fileStream.Close();
            }
        }

        public void LoadSettings()
        {

        }

        [XmlIgnore]
        public virtual GMapControlModel MapSettings
        {
            get { return AppBootstrapper.GetInstance<GMapViewModel>().Map; }
            set { AppBootstrapper.GetInstance<GMapViewModel>().Map = value; }
        }

    }
}
