import { fetchPosts } from '@/api/posts';
import { PostCard } from '@/components/post-card';
import { useQuery } from '@tanstack/react-query';
import { createFileRoute } from '@tanstack/react-router';

export const Route = createFileRoute('/_authenticated/posts')({
  component: Posts,
});

function Posts() {
  const { isPending, isError, data, error } = useQuery({
    queryKey: ['posts'],
    queryFn: fetchPosts,
  });

  if (isPending) {
    return <span>Loading...</span>;
  }

  if (isError) {
    return <span>Error: {error.message}</span>;
  }

  return (
    <div className="mx-auto max-w-2xl p-3">
      <div className="space-y-6">
        {data.map((post) => (
          <PostCard post={post} />
        ))}
      </div>
    </div>
  );
}
