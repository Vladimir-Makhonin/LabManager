namespace LabManager.Exceptions
{
    /// <summary>
    /// Represents an attempt to create a relationship
    /// that already exists.
    /// </summary>
    public class DuplicateRelationshipException : Exception
    {
        public DuplicateRelationshipException(string message)
            : base(message)
        {
        }
    }
}