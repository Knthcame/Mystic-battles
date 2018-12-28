namespace PVPMistico.Validation.Rules.Interfaces
{
    public interface IValidationRule<T> where T : class
    {
        string ValidationMessage { get; set; }

        bool Check(T value);
    }
}
