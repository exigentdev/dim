import axios, { AxiosResponse } from 'axios';
import { PostDto } from '../../types/post-dto';
import { CreatePostDto } from '../../types/create-post-dto';
import { LikePostDto } from '../../types/like-post-dto';
import { TOKENKEY } from '@/utils/constants';

const API_BASE_URL = process.env.API_BASE_URL || '';

export const fetchPosts = async (): Promise<PostDto[]> => {
  const token = localStorage.getItem(TOKENKEY);

  const { data } = await axios.get<undefined, AxiosResponse<PostDto[]>>(
    `${API_BASE_URL}/api/post`,
    {
      headers: { Authorization: `Bearer ${token}` },
    },
  );

  return data;
};

export const createPost = async (createPostDto: CreatePostDto) => {
  const token = localStorage.getItem(TOKENKEY);

  const { data } = await axios.post<CreatePostDto>(
    `${API_BASE_URL}/api/post/create`,
    createPostDto,
    {
      headers: { Authorization: `Bearer ${token}` },
    },
  );

  return data;
};

export const likePost = async (likePostDto: LikePostDto) => {
  const token = localStorage.getItem(TOKENKEY);

  const { data } = await axios.post<LikePostDto>(
    `${API_BASE_URL}/api/post/likePost`,
    likePostDto,
    {
      headers: { Authorization: `Bearer ${token}` },
    },
  );

  return data;
};
