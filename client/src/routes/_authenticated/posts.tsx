import { fetchPosts, likePost } from '@/api/posts';
import { PostCard } from '@/components/post-card';
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { createFileRoute } from '@tanstack/react-router';

export const Route = createFileRoute('/_authenticated/posts')({
  component: Posts,
});

function Posts() {
  const queryClient = useQueryClient();

  const { isPending, isError, data, error } = useQuery({
    queryKey: ['posts'],
    queryFn: fetchPosts,
  });

  const { mutate } = useMutation({
    mutationKey: ['likedPost'],
    mutationFn: likePost,
    // TODO: def don't do this, refetches all the posts when liking
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['posts'] });
    },
  });

  const onLikeClick = (postId: number) => {
    mutate({ postId });
  };

  if (isPending) {
    return <span>Loading...</span>;
  }

  if (isError) {
    return <span>Error: {error.message}</span>;
  }

  return (
    <div className="mx-auto max-w-2xl p-3">
      <div className="space-y-6">
        {[...data].reverse().map((post) => (
          <PostCard key={post.id} post={post} onLikeClick={onLikeClick} />
        ))}
      </div>
    </div>
  );
}
