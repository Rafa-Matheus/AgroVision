using System;

namespace AgroVision.Rastering
{
    [Serializable]
    public class RasterImageData
    {

        #region Propriedades
        public string Path { get; set; }

        public double[] Values { get; set; }

        public int Rotation { get; set; }

        public bool IsVisible { get; set; }
        #endregion

        #region Inicializar
        public RasterImageData(string path, double[] values, int rotation, bool isVisible)
        {
            Path = path;
            Values = values;
            Rotation = rotation;
            IsVisible = isVisible;
        }
        #endregion

    }
}