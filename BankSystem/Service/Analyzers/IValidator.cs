namespace Service.Analyzers
{
    public interface IValidator<in T>
    {
        bool Validate(T obj);
    }
}
