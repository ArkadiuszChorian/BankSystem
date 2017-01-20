namespace Service.Analyzers
{
    /// <summary>
    /// Interface for validatiors
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidator<in T>
    {
        bool Validate(T obj);
    }
}
