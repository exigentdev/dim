import { decodeJWT } from '@/utils';
import { TOKENKEY } from '@/utils/constants';

export const useAuth = () => {
  const logout = () => {
    localStorage.removeItem(TOKENKEY);
  };

  const login = (token: string) => {
    localStorage.setItem(TOKENKEY, token);
  };

  const getToken = () => {
    localStorage.getItem(TOKENKEY);
  };

  const isLoggedIn = () => {
    let jwt;
    try {
      jwt = decodeJWT();
    } catch (error) {
      return false;
    }

    return (
      localStorage.getItem(TOKENKEY) !== null && jwt.exp * 1000 > Date.now()
    );
  };

  return { login, logout, isLoggedIn, getToken };
};

export type AuthContext = ReturnType<typeof useAuth>;
