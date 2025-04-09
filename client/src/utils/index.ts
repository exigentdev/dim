import { TOKENKEY } from './constants';

interface JWTClaims {
  given_name: string;
  email: string;
  exp: number;
  sub: string;
}

export function decodeJWT(): JWTClaims {
  const token = localStorage.getItem(TOKENKEY);

  if (!token) {
    throw Error('Token not found');
  }

  try {
    let base64Url = token.split('.')[1];
    let base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    let jsonPayload = decodeURIComponent(
      window
        .atob(base64)
        .split('')
        .map(function (c) {
          return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        })
        .join(''),
    );

    return JSON.parse(jsonPayload);
  } catch (error) {
    throw Error('Invalid token');
  }
}
