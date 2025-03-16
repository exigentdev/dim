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

  const isLoggedIn = () => localStorage.getItem(TOKENKEY) !== null;

  return { login, logout, isLoggedIn, getToken };
};

export type AuthContext = ReturnType<typeof useAuth>;
