namespace SSResurrezioneBR.Models.Exceptions.Application
{
    public class OptimisticConcurrencyException : Exception
    {
        public OptimisticConcurrencyException() : base($"Non é stato possibile aggiornare le modifiche perché nel frattempo ci sono stati aggiornamenti da parte di un altro utente")
        {

        }
    }
}
