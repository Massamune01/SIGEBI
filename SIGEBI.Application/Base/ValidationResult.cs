namespace SIGEBI.Application.Base
{
    public class ValidationResult
    {
        public List<string> Errors { get; } = new();
        public bool IsValid => !Errors.Any();
        public void AddError(string msg) => Errors.Add(msg);
    }
}
