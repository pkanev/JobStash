namespace JobStash.Application.Common.Exceptions;

public class DuplicatingTechnologyExcpetion : Exception
{
    public DuplicatingTechnologyExcpetion(string technology)
        : base($"A technology with the name \"{technology}\" already exists.")
    {
    }
}
