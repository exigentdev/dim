import {
  createFileRoute,
  ErrorComponentProps,
  redirect,
} from '@tanstack/react-router';
import { fetchPosts } from '../../api/fetchPosts';

export const Route = createFileRoute('/_authenticated/posts')({
  component: Posts,
  beforeLoad: async ({ context }) => {
    const { isLogged } = context.authentication;
    if (!isLogged()) {
      throw redirect({ to: '/login' });
    }
  },
  loader: fetchPosts,
  errorComponent: Error,
});

function Posts() {
  const posts = Route.useLoaderData();
  console.log(posts.appUser);
  console.log('hello');
  return <div>Hello "/authenticated/posts"!</div>;
}

function Error(props: ErrorComponentProps) {
  console.log(props);
  return <div>error: {props.error.message}</div>;
}
