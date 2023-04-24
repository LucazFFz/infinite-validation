using InfiniteValidation;
using InfiniteValidation.Sample;

var validator = new CanDrinkValidator();

var person = new Person("Lucaz", null, 17);

var result = validator.Validate(person, settings =>
{
    settings.ThrowExceptionOnInvalid = true;
});

Console.WriteLine(result.IsValid);