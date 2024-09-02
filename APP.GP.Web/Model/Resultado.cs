namespace APP.GP.Web.Model
{
    public class Resultado
    {
        public int ProcesoExitoso { get; set; }

        public string Mensaje { get; set; }
    }

    public class Resultado<T>
    {
        public int ProcesoExitoso { get; set; }

        public string Mensaje { get; set; }

        public T Objeto { get; set; }
    }
}
