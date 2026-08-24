namespace LabManager.Exceptions
{
    /// <summary>
    /// Represents an error caused by requesting an entity
    /// that does not exist.
    /// </summary>
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message)
            : base(message)
        {
        }
    }
}