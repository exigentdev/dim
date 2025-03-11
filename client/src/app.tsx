import { RouterProvider, createRouter } from '@tanstack/react-router';
import { routeTree } from './routeTree.gen';
import { useAuth } from './hooks/auth';

declare module '@tanstack/react-router' {
  interface Register {
    router: typeof router;
  }
}

const router = createRouter({
  routeTree,
  context: { authentication: undefined! },
});

export function App() {
  const authentication = useAuth();

  return <RouterProvider router={router} context={{ authentication }} />;
}
