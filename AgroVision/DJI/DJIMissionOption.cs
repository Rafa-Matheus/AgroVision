namespace AgroVision
{
    public class DJIMissionOption
    {

        #region Propriedades
        public string Name { get; set; }

        public string Value { get; set; }
        #endregion

        #region Inicializar
        public DJIMissionOption(string name, string value)
        {
            Name = name;
            Value = value;
        }
        #endregion

    }
}