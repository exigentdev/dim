import { createFileRoute, ErrorComponentProps } from '@tanstack/react-router';
import { fetchPosts } from '../../api/fetchPosts';

export const Route = createFileRoute('/_authenticated/posts')({
  component: Posts,
  loader: fetchPosts,
  errorComponent: Error,
});

function Posts() {
  const posts = Route.useLoaderData();
  console.log(posts.appUser);
  return <div>Hello "/authenticated/posts"!</div>;
}

function Error(props: ErrorComponentProps) {
  console.log(props);
  return <div>error: {props.error.message}</div>;
}
