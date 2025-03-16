import axios, { AxiosResponse } from 'axios';
import { PostDto } from '../../types/post-dto';
const TOKENKEY = 'jwt-token';

export const fetchPosts = async (): Promise<PostDto[]> => {
  const token = localStorage.getItem(TOKENKEY);

  const { data } = await axios.get<undefined, AxiosResponse<PostDto[]>>(
    '/api/post',
    {
      headers: { Authorization: `Bearer ${token}` },
    },
  );

  return data;
};
