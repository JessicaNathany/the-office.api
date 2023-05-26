using the_office.domain.Enums;

namespace the_office.domain.Errors;

public class Error : IEquatable<Error>
{
    public static readonly Error None = new(ErrorType.None, string.Empty);
    public static readonly Error NullValue = new(ErrorType.NullValue, "The specified result value is null.");

    public Error(ErrorType code, string message)
    {
        Code = code;
        Message = message;
    }

    public ErrorType Code { get; }
    public string Message { get; }

    public static implicit operator ErrorType(Error error) => error.Code;

    public static bool operator ==(Error? a, Error? b)
    {
        if (a is null && b is null) 
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Error? a, Error? b)
    {
        return !(a == b);
    }

    public bool Equals(Error? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Code == other.Code && Message == other.Message;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        
        return obj.GetType() == GetType() && Equals((Error) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Code, Message);
    }
}