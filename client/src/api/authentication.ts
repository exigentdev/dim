import axios, { AxiosResponse } from 'axios';
import { LoginDto } from 'types/login-dto';
import { NewUserDto } from 'types/new-user-dto';
import { RegisterDto } from 'types/register-dto';

const API_BASE_URL = process.env.API_BASE_URL || '';

export const loginUser = async (loginInfo: LoginDto): Promise<NewUserDto> => {
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
