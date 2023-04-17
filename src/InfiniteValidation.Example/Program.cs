using InfiniteValidation;
using InfiniteValidation.Models;
using ValidationLibrary.Console;

var validator = new CanDrinkValidator();

var person = new Person("Lucaz", null, 17);

var options = new ValidationOptions
{
    ThrowExceptionOnInvalid = true
};

var result = validator.Validate(person, options);

Console.WriteLine(result.IsValid);