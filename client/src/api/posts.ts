import axios, { AxiosResponse } from 'axios';
import { PostDto } from '../../types/post-dto';
import { CreatePostDto } from '../../types/create-post-dto';
import { TOKENKEY } from '@/utils/constants';

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

export const createPost = async (createPostDto: CreatePostDto) => {
  const token = localStorage.getItem(TOKENKEY);

  const { data } = await axios.post<CreatePostDto>(
    '/api/post/create',
    createPostDto,
    {
      headers: { Authorization: `Bearer ${token}` },
    },
  );

  return data;
};
