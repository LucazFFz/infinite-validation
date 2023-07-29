namespace InfiniteValidation;

public sealed class ValidatorConfiguration
{
    public CascadeMode RuleLevelDefaultCascadeMode { get; set; } = CascadeMode.Continue;
    
    public CascadeMode ClassLevelDefaultCascadeMode { get; set; } = CascadeMode.Continue;
}