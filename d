[1mdiff --git a/src/InfiniteValidation/Internal/Rule.cs b/src/InfiniteValidation/Internal/Rule.cs[m
[1mindex 762851a..ecdb8a9 100644[m
[1m--- a/src/InfiniteValidation/Internal/Rule.cs[m
[1m+++ b/src/InfiniteValidation/Internal/Rule.cs[m
[36m@@ -6,23 +6,28 @@[m [mnamespace InfiniteValidation.Internal;[m
 internal class Rule<T, TProperty> : IRule<T, TProperty>[m
 {[m
     private readonly Expression<Func<T, TProperty>> _expression;[m
[32m+[m[41m    [m
[32m+[m[32m    public CascadeMode CascadeMode { get; set; }[m
 [m
[31m-    private readonly List<ISpecification<T, TProperty>> _specifications = new();[m
[31m-[m
[31m-    public Rule(Expression<Func<T, TProperty>> expression)[m
[31m-    {[m
[32m+[m[32m    public List<ISpecification<T, TProperty>> Specifications { get; set; } = new();[m
[32m+[m[41m    [m
[32m+[m[32m    public Rule(Expression<Func<T, TProperty>> expression, Models.CascadeMode cascadeMode)[m
[32m+[m[32m    {[m[41m [m
[32m+[m[32m        CascadeMode = cascadeMode;[m
         _expression = expression;[m
     }[m
     [m
     public IEnumerable<ValidationFailure> IsValid(ValidationContext<T> context)[m
     {[m
[32m+[m[32m        var failures = new List<ValidationFailure>();[m
         var value = _expression.Compile()(context.InstanceToValidate);[m
[31m-        return (from specification in _specifications [m
[31m-            where !specification.IsSatisfiedBy(context, value) [m
[31m-            select specification.GetValidationFailure(value)).ToList();[m
[31m-    }[m
 [m
[31m-    public void AddSpecification(ISpecification<T, TProperty> specification) => _specifications.Add(specification);[m
[32m+[m[32m        foreach (var specification in Specifications.Where(specification => !specification.IsSatisfiedBy(context, value)))[m
[32m+[m[32m        {[m
[32m+[m[32m            failures.Add(specification.GetValidationFailure(value));[m
[32m+[m[32m            if (CascadeMode == CascadeMode.Stop) return failures;[m
[32m+[m[32m        }[m
 [m
[31m-    public List<ISpecification<T, TProperty>> GetSpecifications() => _specifications;[m
[32m+[m[32m        return failures;[m
[32m+[m[32m    }[m
 }[m
\ No newline at end of file[m
