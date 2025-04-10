import axios, { AxiosResponse } from 'axios';
import { LoginDto } from 'types/login-dto';
import { NewUserDto } from 'types/new-user-dto';
import { RegisterDto } from 'types/register-dto';

const API_BASE_URL = import.meta.env.VITE_API_URL || 'vid';

export const loginUser = async (loginInfo: LoginDto): Promise<NewUserDto> => {
  console.log(`${API_BASE_URL}/api/account/login`);
  const { data } = await axios.post<LoginDto, AxiosResponse<NewUserDto>>(
    `${API_BASE_URL}/api/account/login`,
    loginInfo,
  );

  return data;
};

export const registerUser = async (
  registerInfo: RegisterDto,
): Promise<NewUserDto> => {
  const { data } = await axios.post<RegisterDto, AxiosResponse<NewUserDto>>(
    `${API_BASE_URL}/api/account/register`,
    registerInfo,
  );

  return data;
};
