import { createFileRoute, redirect } from '@tanstack/react-router';

export const Route = createFileRoute('/')({
  component: Index,
  beforeLoad: async ({ context }) => {
    const { isLoggedIn } = context.authentication;
    if (isLoggedIn()) {
      throw redirect({ to: '/posts' });
    } else {
      throw redirect({ to: '/login' });
    }
  },
});

function Index() {
  return <></>;
}
