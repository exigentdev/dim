import { PostDto } from '../../types/post-dto';

export const fetchPosts = async (): Promise<PostDto> => {
  const res = await fetch('/api/post/1');
  if (!res.ok) throw new Error('Failed to fetch posts');
  const data = res.json();
  return data as unknown as PostDto;
};
