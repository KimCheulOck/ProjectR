using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginModel
{
    public bool CheckId(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            // 아이디를 입력해주세요.
            return false;
        }

        return true;
    }

    public bool CheckPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            // 비밀번호를 입력해주세요.
            return false;
        }

        if (password.Length < 6)
        {
            // 아이디는 최소 6자리 이상으로 입력해주세요.
            return false;
        }

        if (password.Length > 12)
        {
            // 아이디는 최대 12자리 이하로 입력해주세요.
            return false;
        }

        return true;
    }

    public bool CheckCheckedPassword(string password, string checkedPassword)
    {
        if (password == checkedPassword)
            return true;
        
        // 비밀번호 확인이 올바르지 않습니다.
        return false;
    }

    public bool CheckChangePassword(string password, string checkedPassword)
    {
        if (password == checkedPassword)
        {
            // 변경전의 비밀번호와 동일합니다.
            return false;
        }

        return true;
    }

    public bool CheckPin(string pin)
    {
        if (string.IsNullOrEmpty(pin))
        {
            // PIN 번호를 입력해주세요.
            return false;
        }

        if (pin.Length != 6)
        {
            // PIN 번호는 최소 6자리 숫자로 입력해주세요,
            return false;
        }

        return true;
    }
}
