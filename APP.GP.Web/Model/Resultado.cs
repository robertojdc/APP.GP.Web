namespace APP.GP.Web.Model;

    public class Resultado
    {
        public int ProcesoExitoso { get; set; }

        public string Mensaje { get; set; }

    public int Habilitar { get; set; }

    public int IdEmpresa { get; set; }

    public int Numero { get; set; }

    public string Cadena { get; set; }
    }

    public class Resultado<T>
    {
    public Resultado()
    {
    }

        public int ProcesoExitoso { get; set; }

        public string Mensaje { get; set; }

    public List<T> Lista { get; set; }

        public T Objeto { get; set; }

    public string Cadena { get; set; }
}
