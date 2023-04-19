using InfiniteValidation;
using InfiniteValidation.Sample;

var validator = new CanDrinkValidator();

var person = new Person("Lucaz", null, 17);

var options = new ValidationOptions
{
    ThrowExceptionOnInvalid = false
};

var result = validator.Validate(person, options);

Console.WriteLine(result.IsValid);