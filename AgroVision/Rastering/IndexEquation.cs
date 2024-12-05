namespace AgroVision
{
    public class IndexEquation
    {

        #region Propriedades
        public string Name { get; set; }

        public string Equation { get; set; }

        public string Description { get; set; }
        #endregion

        #region Inicializar
        public IndexEquation(string name, string equation, string description)
        {
            Name = name;
            Equation = equation;
            Description = description;
        }
        #endregion

        #region Métodos
        public override string ToString()
        {
            return Name;
        }
        #endregion

    }
}