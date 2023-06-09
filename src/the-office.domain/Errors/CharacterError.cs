﻿using the_office.domain.Enums;

namespace the_office.domain.Errors
{
    public static class CharacterError
    {
        public static readonly Error NotFound = new(ErrorType.ResourceNotFound, "Character not found");
        public static readonly Error Exists = new(ErrorType.ExistingRegister, "Character already exists");
        public static readonly Error ErrorWhenRegister = new(ErrorType.ErrorWhenExecuting, "Error when trying register character");
    }
}
