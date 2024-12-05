using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace AgroVision
{
    [Serializable]
    public class GeoTag
    {

        public GeoTag(string file_path)
        {
            Latitude = double.NaN;
            Longitude = double.NaN;
            Altitude = double.NaN;

            //15 é o número de decimais máximo
            string gpsPosition = GetCmdOutput(@".\tools\exiftool.exe", @".\tools", $"-c '%.15f' -T -GPSPosition \"{file_path}\"");
            MatchCollection position = new Regex("([0-9.]+)").Matches(gpsPosition);

            //Possui coordenadas
            if (position.Count == 2)
            {
                Latitude = double.Parse(position[0].Value.Replace(".", ","));

                //Lado sul
                if (gpsPosition.Contains("S"))
                    Latitude *= -1;

                Longitude = double.Parse(position[1].Value.Replace(".", ","));

                //Lado oeste
                if (gpsPosition.Contains("W"))
                    Longitude *= -1;
            }

            string gps_altitude = GetCmdOutput(@".\tools\exiftool.exe", @".\tools", $"-T -GPSAltitude \"{file_path}\"");
            MatchCollection altitude = new Regex("([0-9.]+)").Matches(gps_altitude);

            //Possui altitude
            if (altitude.Count == 1)
                Altitude = double.Parse(altitude[0].Value.Replace(".", ","));

            string imgDate = GetCmdOutput(@".\tools\exiftool.exe", @".\tools", $"-T -DateTimeOriginal \"{file_path}\"");
            MatchCollection date = new Regex("[0-9]{4}:[0-9]{2}:[0-9]{2}").Matches(imgDate);

            //Possui data
            if (date.Count == 1)
                Date = DateTime.ParseExact(date[0].Value, "yyyy:MM:dd", CultureInfo.InvariantCulture);
        }

        private string GetCmdOutput(string filePath, string working, string arguments)
        {
            Process process = new Process();
            process.StartInfo.FileName = filePath;
            process.StartInfo.Arguments = arguments; //15 é o número de decimais máximo
            process.StartInfo.WorkingDirectory = working;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            StringBuilder builder = new StringBuilder();
            while (!process.StandardOutput.EndOfStream)
            {
                string line = process.StandardOutput.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;

                builder.Append($"\n{line}");
            }

            return builder.ToString();
        }

        public DateTime Date { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Altitude { get; set; }

        public override string ToString()
        {
            return $"Lat: {Latitude:N13} Lng: {Longitude:N13}";
        }

    }
}