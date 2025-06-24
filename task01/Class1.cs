using System;
namespace task01;

public static class StringExtensions
{
    public static bool IsPalindrome(this string s)
    {
        if (string.IsNullOrEmpty(s))
            return true;

        var transformation = string.Concat(s.ToLower().Where(c => !char.IsWhiteSpace(c) && !char.IsPunctuation(c)));

        for (int i = 0, j = transformation.Length - 1; i < j; i++, j--)
        {
            if (transformation[i] != transformation[j])
                return false;
        }
        return true;
    }
}
