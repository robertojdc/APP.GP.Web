namespace APP.GP.Web.Model.Filtros
{
    public class ActorInvitacionRequest
    {
        public int IdRegistroInvitacion { get; set; }
        public int IdActor { get; set; }
        public bool Propuesta { get; set; }
        public bool Vinculado { get; set; }
    }
}
